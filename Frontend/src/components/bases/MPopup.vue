<template>
	<div class="popup">
		<transition name="zoom" appear>
			<div class="popup-inner" :style="`--mw: ${maxW}px`">
				<div class="popup-header" v-if="showHeader">
					<div class="popup-title">
						<slot name="title" />
					</div>
					<div class="popup-icons">
						<m-icon v-tooltip="'Đóng (ESC)'" icon="close" @click="$emit('close')" />
						<m-icon icon="help" v-tooltip="'Giúp (F1)'" />
					</div>
				</div>
				<div class="popup-content">
					<slot />
				</div>
				<div class="popup__divider"></div>
				<div class="popup-footer">
					<slot name="footer" />
				</div>
			</div>
		</transition>
	</div>
</template>
<script>
import MIcon from "./MIcon.vue";
export default {
	components: {
		MIcon,
	},
	emits: ["close"],
	props: {
		/**
		 * max width popup
		 * @default 900
		 */
		maxW: {
			type: Number,
			default: 900,
		},
		/**
		 * Hiện header
		 * @default true
		 */
		showHeader: {
			type: Boolean,
			default: true,
		},
	},
};
</script>
<style scoped src="../../styles/components/bases/popup.css"></style>
