import dayjs from "dayjs";
import { DateFormat } from "./formatter";
/**
 * @callback validator
 * @param {string} msg - Thông báo lỗi
 * @returns {boolean|string} - true nếu hợp lệ, thông báo lỗi nếu không hợp lệ
 */

/**
 * @type {validator}
 * @author Createdby: nvchung (16/02/2022)
 */
const isRequired = (msg) => {
	return (val) => {
		if (val) return true;
		return msg;
	};
};
/**
 * @type {validator}
 * @author Createdby: nvchung (16/02/2022)
 */
const isEmail = (msg) => {
	return (val) => {
		if (!val) return true;
		return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(val) ? true : msg;
	};
};
/**
 * @type {validator}
 * @param {Date} cmpVal - Date so sánh
 * @author Createdby: nvchung (16/02/2022)
 */
const isDateLessThan = (msg, cmpVal) => {
	return (val) => {
		if (!val) return true;
		return dayjs(val, DateFormat.DMY).isBefore(cmpVal) ? true : msg;
	};
};
export { isRequired, isEmail, isDateLessThan };
