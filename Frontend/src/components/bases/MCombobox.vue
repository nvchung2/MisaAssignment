<template>
	<div :class="['dropdown combobox', error && 'error', top && 'dropdown--top']" ref="combobox">
		<div class="combobox__main">
			<div class="combobox__input">
				<input
					:name="name"
					ref="input"
					type="text"
					autocomplete="off"
					:value="search"
					@focus="handleFocus($event)"
					@keydown.up="handleKeyUp"
					@keydown.down="handleKeyDown"
					@input="handleInput"
					@keydown.enter="handleEnter"
					:title="error"
					:readonly="readonly"
					@blur="$emit('blur', $event)"
				/>
			</div>
			<div :class="['combobox__arrow', active && top && 'combobox__arrow--up']" @click="toggle">
				<m-icon icon="arrow-down-black" :size="16" />
			</div>
		</div>
		<transition :name="top ? 'combobox__slide-down' : 'combobox__slide-up'">
			<div class="dropdown__content dropdown__content--full" v-if="active">
				<m-list v-if="loading">
					<m-list-item> Đang tải... </m-list-item>
				</m-list>
				<div class="combobox__menu-table" v-else-if="tableMenu">
					<table>
						<thead>
							<th v-for="({ text }, index) in headers" :key="index">
								{{ text }}
							</th>
						</thead>
						<tbody v-if="hasItems()">
							<tr
								:class="{
									active: isSelected(item),
									hovered: hoveredIndex == index,
								}"
								v-for="(item, index) in filteredItems"
								:key="index"
								@click="handleItemClick(item)"
							>
								<td v-for="({ value }, index) in headers" :key="index">
									{{ item[value] }}
								</td>
							</tr>
						</tbody>
						<tbody v-else>
							<tr>
								<td :colspan="headers.length" class="combobox__no-result">
									Không có dữ liệu hiển thị
								</td>
							</tr>
						</tbody>
					</table>
				</div>
				<m-list v-else-if="hasItems()">
					<m-list-item
						v-for="(item, index) in filteredItems"
						:key="index"
						:active="isSelected(item)"
						:hovered="index == hoveredIndex"
						@click="handleItemClick(item)"
						>{{ getLabel(item) }}
					</m-list-item>
				</m-list>
				<m-list v-else>
					<div class="combobox__no-result">Không có dữ liệu hiển thị</div>
				</m-list>
			</div>
		</transition>
	</div>
</template>
<script>
import MList from "./List/MList.vue";
import MListItem from "./List/MListItem.vue";
import MIcon from "./MIcon.vue";
export default {
	components: { MList, MListItem, MIcon },
	props: {
		/**
		 * Tên
		 */
		name: String,
		/**
		 * Lỗi
		 */
		error: String,
		/**
		 * Hiện loading
		 */
		loading: Boolean,
		/**
		 * Dữ liệu của menu
		 */
		items: {
			type: Array,
			default: () => [],
		},
		/**
		 * Tên thuộc tính hiển thị trên input
		 */
		labelMember: String,
		/**
		 * Tên thuộc tính làm key so sánh giữa các item
		 */
		valueMember: String,
		/**
		 * @model
		 */
		modelValue: [String, Number],
		/**
		 * Hàm lọc item
		 */
		filterFn: Function,
		/**
		 * Menu dạng table
		 */
		tableMenu: Boolean,
		/**
		 * Headers của table
		 */
		headers: Array,
		/**
		 * combobox chỉ đọc
		 */
		readonly: Boolean,
		/**
		 * Item được chọn
		 */
		initialSelected: Object,
		/**
		 * Hiển thị menu phía trên
		 */
		top: Boolean,
	},
	emits: ["update:modelValue", "select", "open", "close", "blur", "input"],
	data() {
		return {
			active: false, //hiện menu drop
			selected: this.initialSelected, //item được chọn
			hoveredIndex: -1, //con trỏ chọn
			showAll: false, //hiện tất cả item
			search: this.modelValue || "", //giá trị input
		};
	},
	computed: {
		//items sau khi lọc
		filteredItems() {
			//bỏ qua hàm lọc nếu showAll=true/input rỗng/combobox chỉ đọc
			if (this.showAll || !this.search || this.readonly) {
				return this.items;
			}
			return this.items.filter((item) => this.filterFn(item, this.search));
		},
	},
	methods: {
		handleItemClick(item) {
			this.setSelected(item);
			this.closeDropdown();
		},
		//đóng mở drop menu
		toggle() {
			if (this.active) {
				this.showAll = false;
				this.closeDropdown();
			} else {
				this.showAll = true;
				this.openDropdown(true);
			}
		},
		//kiểm tra có item hiển thị không
		hasItems() {
			return this.filteredItems.length > 0;
		},
		//kiểm tra item có đang được chọn
		isSelected(item) {
			if (typeof item == "object" && this.valueMember) {
				return this.selected?.[this.valueMember] == item[this.valueMember];
			}
			return item == this.selected;
		},
		//bấm nút enter set selectedItem=item ở vị trí con trỏ
		handleEnter() {
			if (this.hoveredIndex != -1 && this.hoveredIndex < this.filteredItems.length) {
				this.setSelected(this.filteredItems[this.hoveredIndex]);
			} else {
				this.setSelected(null);
			}
			this.closeDropdown();
		},
		//nhập trên input
		handleInput(evt) {
			this.setSearch(evt.target.value); //set search
			this.setHoveredIndex(this.items); //update con trỏ chọn
			if (!this.readonly) {
				this.setSelected(null); //xóa selected khi đang nhập dữ liệu
			}
			//mở menu drop
			if (!this.active) {
				this.openDropdown();
			}
			//áp dụng lọc dữ liệu
			if (this.showAll) {
				this.showAll = false;
			}
			this.$emit("input", evt);
		},
		//cập nhật input và emit update model
		setSearch(text) {
			this.search = text;
			this.$emit("update:modelValue", this.search);
		},
		/**
		 * Mở menu drop
		 * @param {Boolean} focus - focus vào input khi mở menu drop
		 */
		openDropdown(focus = false) {
			this.active = true;
			this.setHoveredIndex(this.items);
			this.$emit("open");
			focus && this.focusInput();
		},
		//đóng menu drop
		closeDropdown() {
			this.active = false;
			this.hoveredIndex = -1;
			this.$emit("close");
			this.$nextTick(() => {
				this.$refs.input.blur();
			});
		},
		//focus input
		focusInput() {
			this.$nextTick(() => {
				this.$refs.input.focus(); //focus input
				this.$refs.input.select(); //boi den chu trong input
			});
		},
		/**
		 * Lấy text hiển thị từ item
		 * @param {Object} item
		 */
		getLabel(item) {
			return typeof item == "object" ? item?.[this.labelMember] : item;
		},
		/**
		 * Cập nhật item được chọn
		 * @param {Object} item - item được chọn
		 * @param {boolean} emit - emit sự kiện select
		 */
		setSelected(item, emit = true) {
			this.selected = item;
			if (item != null) {
				this.setSearch(this.getLabel(this.selected));
			}
			emit && this.$emit("select", this.selected);
		},
		//focus text trên input
		handleFocus(evt) {
			evt.target.select();
		},
		//bấm mũi tên lên
		handleKeyUp() {
			this.hoveredIndex = Math.max(0, this.hoveredIndex - 1);
		},
		//bấm mũi tên xuống
		handleKeyDown() {
			this.hoveredIndex = Math.min(this.items.length - 1, this.hoveredIndex + 1);
		},
		//kích ra ngoài parent dom
		handleClickOutside(evt) {
			const target = evt.target;
			const isClickOutside = !(
				this.$refs.combobox == target || this.$refs.combobox.contains(target)
			);
			if (isClickOutside) {
				this.closeDropdown();
			}
		},
		//cập nhật lại con trỏ
		setHoveredIndex(items) {
			this.hoveredIndex = items.length > 0 ? 0 : -1;
		},
	},
	mounted() {
		document.addEventListener("click", this.handleClickOutside);
	},
	unmounted() {
		document.removeEventListener("click", this.handleClickOutside);
	},
	watch: {
		//cập nhật lại con trỏ khi items thay đổi
		items(val) {
			this.setHoveredIndex(val);
		},
		//cập nhật lại selected item
		initialSelected: {
			immediate: true,
			handler(val) {
				this.setSelected(val, false);
			},
		},
		modelValue(val) {
			this.setSearch(val);
		},
	},
};
</script>
<style>
@import url(../../styles/components/bases/dropdown.css);
@import url(../../styles/components/bases/combobox.css);
</style>
