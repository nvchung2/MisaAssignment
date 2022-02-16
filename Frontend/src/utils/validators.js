import dayjs from "dayjs";
import { DateFormat } from "./formatter";
/**
 * @callback validator
 * @param {string} msg - Thông báo lỗi
 * @returns {boolean|string} - true nếu hợp lệ, thông báo lỗi nếu không hợp lệ
 */

/**
 * @type {validator}
 */
const isRequired = (msg) => {
	return (val) => {
		if (val) return true;
		return msg;
	};
};
/**
 * @type {validator}
 */
const isEmail = (msg = "Email không hợp lệ") => {
	return (val) => {
		if (!val) return true;
		return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val) ? true : msg;
	};
};
/**
 * @type {validator}
 */
const isPhoneNumber = (msg) => {
	return (val) => {
		if (!val) return true;
		return /^\d{10,12}$/.test(val) ? true : msg;
	};
};
/**
 * @type {validator}
 * @param {Date} cmpVal - Date so sánh
 */
const isDateLessThan = (msg, cmpVal) => {
	return (val) => {
		if (!val) return true;
		return dayjs(val, DateFormat.DMY).isBefore(cmpVal) ? true : msg;
	};
};
/**
 * @type {validator}
 */
const isNumber = (msg) => {
	return (val) => {
		if (!val) return true;
		return /^\d+$/.test(val) ? true : msg;
	};
};
export { isRequired, isEmail, isPhoneNumber, isDateLessThan, isNumber };
