<template>
	<div ref="dropdown" class="dropdown">
		<div class="dropdown__trigger" @click.stop="toggle">
			<slot name="trigger" :active="active" />
		</div>
		<teleport to="body" v-if="active && appendToBody">
			<div role="menu" :style="styles">
				<slot />
			</div>
		</teleport>
		<div
			:class="['dropdown__content', contentFull && 'dropdown__content--full']"
			v-else-if="active"
		>
			<slot />
		</div>
	</div>
</template>
<script>
export default {
	props: {
		/**
		 * đóng dropdown khi click
		 */
		closeOnClick: {
			type: Boolean,
			default: true,
		},
		/**
		 * teleport đến body
		 */
		appendToBody: Boolean,
		/**
		 * minwidth=100%
		 */
		contentFull: {
			type: Boolean,
			default: true,
		},
	},
	methods: {
		//ẩn/hiện dropdown
		toggle() {
			this.active = !this.active;
			//trong trường hợp teleport đến body, tính toán vị trí hiển thị
			if (this.active && this.appendToBody) {
				const { right, bottom, width } = this.$refs.dropdown.getBoundingClientRect();
				this.styles = {
					left: `${right}px`,
					top: `${bottom}px`,
					minWidth: `${width}px`,
					position: "absolute",
					transform: "translateX(-100%)",
					zIndex: 105,
				};
			}
		},
		//kích ra ngoài parent dom
		handleClickOutside(evt) {
			const target = evt.target;
			const isClickOutside = !(
				this.$refs.dropdown == target || this.$refs.dropdown.contains(target)
			);
			if (isClickOutside || this.closeOnClick) {
				this.active = false;
			}
		},
	},
	data() {
		return {
			active: false, //hiện dropdown
			styles: {}, //style của dropdown menu
		};
	},
	created() {
		document.addEventListener("click", this.handleClickOutside);
	},
	unmounted() {
		document.removeEventListener("click", this.handleClickOutside);
	},
};
</script>

<style>
@import url(../../styles/components/bases/dropdown.css);
</style>
