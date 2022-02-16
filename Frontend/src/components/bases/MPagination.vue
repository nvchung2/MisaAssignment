<template>
	<ul class="page-list">
		<li @click="prevPage" :class="['page-item', page == 1 && 'page-item--disabled']">Trước</li>
		<li
			:class="['page-item', p == page && 'page-item--active', p == '...' && 'page-item--dot']"
			v-for="(p, index) in pages"
			:key="index"
			@click="page = p"
		>
			{{ p }}
		</li>
		<li @click="nextPage" :class="['page-item', page == pageCount && 'page-item--disabled']">
			Sau
		</li>
	</ul>
</template>
<script>
export default {
	props: {
		/**
		 * Tổng số item
		 */
		itemCount: Number,
		/**
		 * Số item/trang
		 */
		perPage: Number,
		/**
		 * Số trang tối đa được hiển thị
		 * @default 3
		 */
		maxDisplayPage: {
			type: Number,
			default: 3,
		},
		/**
		 * Số trang hiện tại
		 * @model
		 * @default 1
		 */
		modelValue: {
			type: Number,
			default: 1,
		},
	},
	emits: ["update:modelValue"],
	methods: {
		// Nhảy đến trang kế
		nextPage() {
			this.page = Math.min(this.page + 1, this.pageCount);
		},
		//Nhảy đến trang trước
		prevPage() {
			this.page = Math.max(this.page - 1, 1);
		},
	},
	computed: {
		page: {
			get() {
				return this.modelValue;
			},
			set(val) {
				this.$emit("update:modelValue", val);
			},
		},
		//Tổng số trang
		pageCount() {
			return Math.ceil(this.itemCount / this.perPage);
		},
		//mảng chứa các số trang
		pages() {
			let sp = Math.max(1, this.page - ~~(this.maxDisplayPage / 2)); //trang bắt đầu hiển thị
			let ep = Math.min(this.pageCount, sp + this.maxDisplayPage - 1); //trang kết thúc hiển thị
			//kiểm tra số trang được hiển thị đã đủ maxDisplayPage chưa
			if (ep - sp + 1 < this.maxDisplayPage) {
				if (sp == 1) {
					ep = Math.min(this.pageCount, sp + this.maxDisplayPage - 1);
				} else if (ep == this.pageCount) {
					sp = Math.max(1, ep - this.maxDisplayPage + 1);
				}
			}
			//khởi tạo mảng pages chứa các số trang
			const pages = Array.from({ length: ep - sp + 1 }, (_, i) => sp + i);
			if (sp > 2) {
				pages.unshift("...");
			}
			if (sp > 1) {
				pages.unshift(1);
			}
			if (ep < this.pageCount - 1) {
				pages.push("...");
			}
			if (ep < this.pageCount) {
				pages.push(this.pageCount);
			}
			return pages;
		},
	},
};
</script>
<style>
@import url(../../styles/components/bases/pagination.css);
</style>
