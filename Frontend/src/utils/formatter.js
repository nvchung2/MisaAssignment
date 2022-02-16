import dayjs from "dayjs";

export const DateFormat = {
	DMY: "DD/MM/YYYY",
	YMD: "YYYY/MM/DD",
	YMDH: "YYYY-MM-DD",
};
/**
 * @author Createdby: nvchung (16/02/2022)
 */
export default {
	formatDate(str, outFormat = DateFormat.DMY, inFormat) {
		if (!str) return "";
		const date = dayjs(str, inFormat);
		return date.isValid() ? date.format(outFormat) : "";
	},
	formatGender(gender) {
		if (gender == 0) return "Nữ";
		return gender == 1 ? "Nam" : "Khác";
	},
	/**
	 * Format string like string.format in .net
	 * @param {string} str - Chuỗi cần format
	 * @param  {...any} args - param format
	 * @returns {string}
	 */
	formatString(str, ...args) {
		return str.replace(/{(\d+)}/g, (match, index) =>
			typeof args[index] != "undefined" ? args[index] : match
		);
	},
};
