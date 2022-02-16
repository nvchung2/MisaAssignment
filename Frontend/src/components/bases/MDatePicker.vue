<template>
	<div :class="['input', 'datepicker', error && 'input--error']">
		<input
			type="text"
			v-model="inputText"
			@input="onInput"
			placeholder="DD/MM/YYYY"
			@blur="onBlur"
			:title="error"
			v-bind="$attrs"
			ref="input"
		/>
		<date-picker
			class="datepicker__icon"
			v-model="date"
			format="dd/MM/yyyy"
			:enableTimePicker="false"
			showNowButton
			nowButtonLabel="Hôm nay"
			selectText="Chọn"
			cancelText="Hủy"
			position="right"
		>
			<template #trigger>
				<m-icon icon="calendar" />
			</template>
			<template #calendar-header="{ index }">
				{{ getDay(index) }}
			</template>
			<template #month="{ value }"> Tháng {{ value + 1 }} </template>
			<template #month-overlay="{ value }">Tháng {{ value + 1 }}</template>
		</date-picker>
	</div>
</template>

<script>
import DatePicker from "vue3-date-time-picker";
import MIcon from "./MIcon.vue";
import dayjs from "dayjs";
import { DateFormat } from "../../utils/formatter";

/**
 * @author Createdby: nvchung (16/02/2022)
 */
export default {
	inheritAttrs: false,
	components: {
		DatePicker,
		MIcon,
	},
	props: {
		/**
		 * Date string value
		 * @model
		 */
		modelValue: String,
		/**
		 * Thông báo lỗi
		 */
		error: String,
	},
	emits: ["update:modelValue"],
	data() {
		return { inputText: this.modelValue };
	},
	setup() {
		const placeholder = "__/__/____";
		const days = ["T2", "T3", "T4", "T5", "T6", "T7", "CN"];
		return { placeholder, days };
	},
	methods: {
		/**
		 * Tên thứ tương tứng với index
		 */
		getDay(index) {
			return this.days[index];
		},
		/**
		 * Kiểm tra hợp lệ chuỗi date
		 * @param {string} str
		 */
		isValidDateString(str) {
			return /\d{2}\/\d{2}\/\d{4}/.test(str);
		},
		/**
		 * Kiểm tra ngày hợp lệ
		 * @param {string} str
		 */
		isValidDate(str) {
			const date = dayjs(str, DateFormat.DMY);
			if (!date.isValid()) return false;
			const dps = str.split("/");
			return date.date() == parseInt(dps[0]) ? true : false;
		},
		/**
		 * reset input text nếu chuỗi date không hợp lệ
		 */
		onBlur() {
			if (!this.isValidDateString(this.inputText)) {
				this.inputText = undefined;
			}
		},
		/**
		 * Nhập date trên input
		 * @param {InputEvent} evt
		 */
		onInput(evt) {
			//Auto format về dạng __/__/____
			const val = evt.target.value;
			this.inputText = this.autoFormat(val);
			//Xóa chuỗi date
			if (this.inputText == this.placeholder) {
				this.$emit("update:modelValue", undefined);
				return;
			}
			//Kiểm tra hợp lệ chuỗi date
			if (this.isValidDateString(this.inputText)) {
				const date = this.isValidDate(this.inputText);
				if (!date) {
					this.inputText = undefined; //ngày không hợp lệ, reset input
				} else {
					this.$emit("update:modelValue", this.inputText); //ngày hợp lệ, emit change
				}
			}
			//cập nhật vị trí con trỏ
			this.$nextTick(() => {
				const pos = this.autoCaret(this.inputText);
				evt.target.setSelectionRange(pos, pos);
			});
		},
		/**
		 * Tính toán vị trí con trỏ
		 */
		autoCaret(str) {
			if (!str) return 0;
			for (let i = str.length; i >= 0; i--) {
				if (/\d/.test(str[i])) {
					return i + 1;
				}
			}
			return 0;
		},
		/**
		 * Tự động format input về dạng __/__/____
		 */
		autoFormat(str) {
			if (!str) return this.placeholder;
			const digits = str.replace(/\D/g, "");
			let res = "",
				i = 0,
				g = 0;
			while (i < digits.length && g < 3) {
				if (g < 2) {
					let s = digits.substring(i, i + 2);
					let sd = parseInt(s);
					if (s.length == 1) sd *= 10;
					if ((g == 0 && sd < 32) || (g == 1 && sd < 13)) {
						res += s.padEnd(2, "_") + "/";
						i++;
						g++;
					}
				} else {
					let s = digits.substring(i, i + 4);
					if (/^[^0]/.test(s)) {
						res += s.padEnd(4, "_");
						i += 3;
						g++;
					}
				}
				i++;
			}
			return (res + this.placeholder.substring(res.length)).substr(0, 10);
		},
	},
	computed: {
		/**
		 * datepicker model
		 */
		date: {
			get() {
				return dayjs(this.modelValue, DateFormat.DMY).toDate();
			},
			/**
			 * @param {Date} val
			 */
			set(val) {
				this.$emit("update:modelValue", dayjs(val).format(DateFormat.DMY));
				this.$nextTick(() => {
					this.$refs.input.dispatchEvent(new Event("input"));
				});
			},
		},
	},
	watch: {
		modelValue(val) {
			this.inputText = val;
		},
	},
};
</script>

<style src="../../styles/components/bases/datepicker.css"></style>
