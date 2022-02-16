<template>
	<transition name="slide">
		<div v-if="show" class="toast-message">
			<m-icon :icon="variant" :size="48" />
			<div class="toast-message__text">
				{{ text }}
			</div>
			<m-icon icon="close-sm" @click="$emit('close')" />
		</div>
	</transition>
</template>

<script>
import MIcon from "../../components/bases/MIcon.vue";
export default {
	props: {
		/**
		 * Kiểu toast
		 * @values success, error
		 * @default success
		 */
		variant: {
			type: String,
			default: "success",
		},
		/**
		 * Hiện toast
		 */
		show: Boolean,
		/**
		 * Nội dung toast
		 */
		text: String,
	},
	components: { MIcon },
	emits: ["close"],
};
</script>

<style scoped src="../../styles/components/bases/toast-message.css"></style>
