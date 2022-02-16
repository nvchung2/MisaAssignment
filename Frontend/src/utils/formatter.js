import dayjs from "dayjs";

export const DateFormat = {
	DMY: "DD/MM/YYYY",
	YMD: "YYYY/MM/DD",
	YMDH: "YYYY-MM-DD",
};
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
};
