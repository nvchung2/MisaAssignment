<template>
	<label class="radio">
		<input type="radio" v-bind="$attrs" v-model="model" />
		<span>
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
		modelValue: [String, Number],
	},
	emits: ["update:modelValue"],
	computed: {
		model: {
			get() {
				return this.modelValue;
			},
			set(val) {
				this.$emit("update:modelValue", val);
			},
		},
	},
};
</script>
<style scoped src="../../styles/components/bases/radio.css"></style>
