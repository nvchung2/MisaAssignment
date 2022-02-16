import { ref } from "vue";

export default function useToastMessage() {
  const toastProps = ref({});
  let id;
  const close = () => {
    toastProps.value = {};
  };
  const makeToast = (text, variant = "success", timeout = 5000) => {
    toastProps.value = { text, variant, show: true };
    if (id) {
      clearTimeout(id);
    }
    id = setTimeout(close, timeout);
  };
  const toastEvents = {
    close,
  };
  return { toastProps, makeToast, toastEvents };
}
