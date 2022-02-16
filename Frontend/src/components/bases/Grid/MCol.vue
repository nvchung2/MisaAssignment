<template>
	<div class="col" :style="style">
		<slot />
	</div>
</template>
<script>
/**
 * @author Createdby: nvchung (16/02/2022)
 */
export default {
	props: {
		/**
		 * số cột mà col chiếm
		 */
		span: Number,
	},
	computed: {
		style() {
			return {
				"--width": `${(this.span * 100) / 12}%`,
				"--grow": this.span ? 0 : 1,
			};
		},
	},
};
</script>
<style scoped src="../../../styles/components/bases/grid.css"></style>
