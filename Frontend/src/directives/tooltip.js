import tippy from "tippy.js";
/**
 * @type {import("vue").Directive}
 * @author Createdby: nvchung (16/02/2022)
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
