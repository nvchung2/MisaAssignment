<template>
	<button :class="classes">
		<slot />
	</button>
</template>
<script>
/**
 * @author Createdby: nvchung (16/02/2022)
 */
export default {
	props: {
		/**
		 * Dạng button
		 * @values contained, text, icon
		 */
		variant: {
			type: String,
			default: "contained",
		},
		/**
		 * Màu button
		 * @values primary, secondary
		 */
		color: {
			type: String,
			default: "primary",
		},
		/**
		 * Kích thước button
		 * @values sm, md
		 */
		size: {
			type: String,
			default: "md",
		},
		/**
		 * Dạng button bo tròn
		 */
		pill: Boolean,
		/**
		 * Hiện outline
		 */
		outlined: Boolean,
	},
	computed: {
		classes() {
			return [
				`btn btn--${this.variant} btn--${this.color} btn--${this.size}`,
				this.pill && "btn--pill",
				this.outlined && "btn--outlined",
			];
		},
	},
};
</script>
<style scoped src="../../styles/components/bases/button.css"></style>
