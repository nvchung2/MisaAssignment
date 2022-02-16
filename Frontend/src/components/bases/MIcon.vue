<template>
	<div
		:class="[`icon icon-${size} icon-${icon}`, anim && `animation-${anim}`]"
		@click="$emit('click')"
	></div>
</template>

<script>
/**
 * @author Createdby: nvchung (16/02/2022)
 */
export default {
	emits: ["click"],
	props: {
		/**
		 * Kích thước icon
		 * @values 16,24,48
		 */
		size: {
			type: Number,
			default: 24,
		},
		/**
		 * Tên icon
		 */
		icon: {
			type: String,
			required: true,
		},
		/**
		 * Tên hiệu ứng
		 */
		anim: String,
	},
};
</script>
<style scoped src="../../styles/components/bases/icon.css"></style>
