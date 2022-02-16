<template>
	<div :class="['input', icon && 'input--icon', error && 'input--error']">
		<input v-bind="$attrs" :title="error" v-model="model" ref="input" />
		<m-icon class="input__icon" v-if="icon" :icon="icon" :size="16" />
	</div>
</template>
<script>
import MIcon from "./MIcon.vue";
export default {
	inheritAttrs: false,
	components: {
		MIcon,
	},
	props: {
		/**
		 * Tên icon
		 */
		icon: String,
		/**
		 * Lỗi
		 */
		error: String,
		/**
		 * @model
		 */
		modelValue: [String, Number],
		/**
		 * Tự động focus on mount
		 */
		autoFocus: Boolean,
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
	mounted() {
		//auto focus input
		if (this.autoFocus) {
			this.$refs.input.focus();
		}
	},
};
</script>
<style scoped src="../../styles/components/bases/input.css"></style>
