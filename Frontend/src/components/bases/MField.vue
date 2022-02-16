<template>
	<div class="field">
		<label class="field__label">
			<span class="field__label-title">{{ label }}</span>
			<span class="field__label-required" v-if="required">*</span>
		</label>
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
		 * label của input
		 */
		label: String,
		/**
		 * trường bắt buộc
		 */
		required: Boolean,
	},
};
</script>
<style scoped src="../../styles/components/bases/field.css"></style>
