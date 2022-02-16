import { createApp } from "vue";
import Tooltip from "./directives/tooltip";
import formatter from "./utils/formatter";
import axios from "axios";
import App from "./App.vue";
import dayjs from "dayjs";
import customParseFormat from "dayjs/plugin/customParseFormat";
import "tippy.js/animations/shift-toward.css";
import "./styles/components/bases/tooltip.css";

dayjs.extend(customParseFormat);
window.dayjs = dayjs;
const app = createApp(App);
app.directive("tooltip", Tooltip);
app.config.globalProperties.$formatters = formatter;
app.config.globalProperties.$axios = axios;
app.mount("#app");
