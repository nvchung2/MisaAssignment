import tippy from "tippy.js";
/**
 * @type {import("vue").Directive}
 */
const Tooltip = {
	mounted(el, { value }) {
		tippy(el, {
			content: value,
			arrow: true,
			animation: "shift-toward",
			placement: "bottom",
		});
	},
	updated(el, { value, oldValue }) {
		if (value != oldValue) {
			el._tippy.setContent(value);
		}
	},
	unmounted(el) {
		el._tippy.destroy();
	},
};
export default Tooltip;
