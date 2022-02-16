<template>
	<div class="table-container">
		<table class="table">
			<thead>
				<th class="th-check-all" v-if="showCheck">
					<m-check-box @change="checkAll($event)" :checked="checkAllChecked" />
				</th>
				<th
					v-for="{ text, classes, propName, width } in computedHeaders"
					:key="propName"
					:class="classes"
					:style="{ minWidth: `${width}px` }"
				>
					{{ text }}
				</th>
				<th v-if="showActions" class="td-actions">chức năng</th>
			</thead>
			<tbody>
				<tr v-for="item in computedItems" :key="item[keyName]">
					<td class="td-check" v-if="showCheck">
						<m-check-box v-model="selectedValues" :value="item[keyName]" />
					</td>
					<td
						v-for="{ propName, classes, width } in computedHeaders"
						:key="propName"
						:class="classes"
						:style="{ minWidth: `${width}px` }"
					>
						{{ item[propName] }}
					</td>
					<td v-if="showActions" class="td-actions">
						<div class="actions">
							<slot name="actions" :item="item" />
						</div>
					</td>
				</tr>
			</tbody>
		</table>
	</div>
</template>
<script>
import MCheckBox from "./MCheckBox.vue";
export default {
	components: {
		MCheckBox,
	},
	props: {
		/**
		 * Header chứa các thuộc tính được hiển thị
		 * @requires
		 */
		headers: {
			type: Array,
			required: true,
		},
		/**
		 * Các bản ghi
		 */
		items: {
			type: Array,
			default: () => [],
		},
		/**
		 * Khóa chính của bản ghi
		 * @requires
		 */
		keyName: {
			type: String,
			required: true,
		},
		/**
		 * Mảng khóa chính được check
		 * @model
		 */
		modelValue: {
			type: Array,
			default: () => [],
		},
	},
	emits: ["update:modelValue"],
	methods: {
		//check all items
		checkAll(evt) {
			if (evt.target.checked) {
				this.selectedValues = this.items.map((item) => item[this.keyName]);
			} else {
				this.selectedValues = [];
			}
		},
	},
	computed: {
		//các giá trị được check
		selectedValues: {
			get() {
				return this.modelValue;
			},
			set(val) {
				this.$emit("update:modelValue", val);
			},
		},
		//headers sau khi format
		computedHeaders() {
			return this.headers.map((header) => {
				const newHeader = { ...header };
				switch (header.format) {
					case "date":
						newHeader.classes = "text-center";
						break;
					case "currency":
						newHeader.classes = "text-right";
						break;
				}
				return newHeader;
			});
		},
		//các bản ghi được format
		computedItems() {
			return this.items.map((item) => {
				const newItem = { ...item };
				for (const { propName, format } of this.headers) {
					switch (format) {
						case "date":
							newItem[propName] = this.$formatters.formatDate(newItem[propName]);
							break;
						case "gender":
							newItem[propName] = this.$formatters.formatGender(newItem[propName]);
					}
				}
				return newItem;
			});
		},
		//kiểm tra tất cả bản ghi được check
		checkAllChecked() {
			if (this.modelValue.length == this.items.length) return true;
			return false;
		},
		//Có hiện cột checkbox?
		showCheck() {
			return this.items.length > 0 && this.headers.length > 0;
		},
		//Có hiện cột actions
		showActions() {
			return !!this.$slots.actions && this.headers.length > 0;
		},
	},
};
</script>
<style scoped src="../../styles/components/bases/table.css"></style>
