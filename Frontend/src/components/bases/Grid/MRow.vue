<template>
	<div class="row" :style="style">
		<slot />
	</div>
</template>
<script>
export default {
	props: {
		/**
		 * Khoảng cách giữa các cột
		 */
		gx: {
			type: Number,
			default: 0,
		},
		/**
		 * Khoảng cách giữa các dòng
		 */
		gy: {
			type: Number,
			default: 0,
		},
	},
	computed: {
		style() {
			return {
				"--gx": `${this.gx}px`,
				"--gy": `${this.gy}px`,
			};
		},
	},
};
</script>
<style scoped src="../../../styles/components/bases/grid.css"></style>
