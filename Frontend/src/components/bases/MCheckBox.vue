<template>
	<label class="checkbox">
		<input ref="input" type="checkbox" v-bind="$attrs" v-model="model" />
		<span v-if="$slots.default">
			<slot />
		</span>
	</label>
</template>
<script>
/**
 * @author Createdby: nvchung (16/02/2022)
 */
export default {
	inheritAttrs: false,
	props: {
		/**
		 * @model
		 */
		modelValue: Array,
	},
	emits: ["update:modelValue"],
	computed: {
		model: {
			get() {
				return this.modelValue;
			},
			set(value) {
				this.$emit("update:modelValue", value);
			},
		},
	},
};
</script>
<style scoped src="../../styles/components/bases/checkbox.css"></style>
