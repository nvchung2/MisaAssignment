<template>
	<div class="message-dialog" v-if="show">
		<div class="message-dialog__inner">
			<div class="message-dialog__body">
				<m-icon :size="48" :icon="icon" />
				<span class="message-dialog__text">{{ text }}</span>
			</div>
			<hr class="message-dialog__line" />
			<div
				:class="['message-dialog__footer gap-10', isConfirm && 'message-dialog__footer--confirm']"
			>
				<m-button v-if="showCancel" color="secondary" @click="$emit('cancel')">Hủy</m-button>
				<m-button
					v-if="isConfirm"
					:class="showCancel && 'ml-auto'"
					color="secondary"
					@click="$emit('no')"
					>Không</m-button
				>
				<m-button v-if="isConfirm" @click="$emit('yes')">Có</m-button>
				<m-button v-if="isInfo" @click="$emit('close')">Đóng</m-button>
			</div>
		</div>
	</div>
</template>
<script>
import MButton from "./MButton.vue";
import MIcon from "./MIcon.vue";
/**
 * @author Createdby: nvchung (16/02/2022)
 */
export default {
	components: { MButton, MIcon },
	props: {
		/**
		 * Tên icon
		 */
		icon: String,
		/**
		 * Nội dung thông báo
		 */
		text: String,
		/**
		 * Kiểu thông báo
		 * @values info, confirm
		 */
		variant: String,
		/**
		 * Hiện dialog
		 */
		show: Boolean,
		/**
		 * Hiện nút hủy
		 */
		showCancel: Boolean,
	},
	emits: ["yes", "no", "close", "cancel"],
	computed: {
		isConfirm() {
			return this.variant == "confirm";
		},
		isInfo() {
			return this.variant == "info";
		},
	},
};
</script>
<style scoped src="../../styles/components/bases/message-dialog.css"></style>
