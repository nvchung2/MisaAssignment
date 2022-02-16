<template>
	<m-popup @close="closeForm">
		<template #title>
			<h2>Thông tin nhân viên</h2>
			<m-check-box style="margin-left: 40px">Là khách hàng</m-check-box>
			<m-check-box style="margin-left: 18px">Là nhà cung cấp</m-check-box>
		</template>
		<form ref="form">
			<m-row :gx="26">
				<m-col :span="6">
					<m-row :gx="6" :gy="12">
						<m-col :span="5">
							<m-field required label="Mã">
								<m-input
									v-model="employee.EmployeeCode"
									name="EmployeeCode"
									:error="getInputError('EmployeeCode')"
									@blur="handleBlur"
									@input="handleInput"
									autoFocus
									maxlength="25"
								/>
							</m-field>
						</m-col>
						<m-col :span="7"
							><m-field required label="Tên">
								<m-input
									v-model="employee.FullName"
									name="FullName"
									:error="getInputError('FullName')"
									@blur="handleBlur"
									@input="handleInput"
									maxlength="200"
								/> </m-field
						></m-col>
						<m-col :span="12">
							<m-field required label="Đơn vị">
								<m-combobox
									name="DepartmentId"
									@blur="handleBlur"
									@input="handleInput"
									:error="getInputError('DepartmentId')"
									style="width: 100%"
									:items="departments"
									:loading="departmentLoading"
									v-model="employee.DepartmentName"
									tableMenu
									:headers="departmentHeaders"
									:filterFn="departmentFilter"
									@select="handleDepartmentSelect"
									@open="loadDepartments"
									labelMember="DepartmentName"
									valueMember="DepartmentId"
									:initialSelected="{
										DepartmentId: employee.DepartmentId,
										DepartmentName: employee.DepartmentName,
									}"
								/>
							</m-field>
						</m-col>
						<m-col :span="12">
							<m-field label="Chức danh">
								<m-input v-model="employee.EmployeePosition" @input="handleInput" maxlength="128" />
							</m-field>
						</m-col>
					</m-row>
				</m-col>
				<m-col :span="6">
					<m-row :gx="6" :gy="12">
						<m-col :span="5">
							<m-field label="Ngày sinh">
								<m-date-picker
									v-model="employee.DateOfBirth"
									name="DateOfBirth"
									:error="getInputError('DateOfBirth')"
									@blur="handleBlur"
									@input="handleInput"
								/>
							</m-field>
						</m-col>
						<m-col :span="7"
							><m-field label="Giới tính" style="padding-left: 10px">
								<div class="flex items-center" style="padding-top: 5px">
									<m-radio value="1" v-model="employee.Gender">Nam</m-radio>
									<m-radio value="0" v-model="employee.Gender">Nữ</m-radio>
									<m-radio value="2" v-model="employee.Gender">Khác</m-radio>
								</div>
							</m-field></m-col
						>
						<m-col :span="7">
							<m-field label="Số CMND">
								<m-input
									v-model="employee.IdentityNumber"
									name="IdentityNumber"
									@input="handleInput"
									maxlength="20"
								/>
							</m-field>
						</m-col>
						<m-col :span="5">
							<m-field label="Ngày cấp">
								<m-date-picker
									v-model="employee.IdentityDate"
									name="IdentityDate"
									:error="getInputError('IdentityDate')"
									@blur="handleBlur"
									@input="handleInput"
								/>
							</m-field>
						</m-col>
						<m-col :span="12">
							<m-field label="Nơi cấp">
								<m-input v-model="employee.IdentityPlace" @input="handleInput" maxlength="255" />
							</m-field>
						</m-col>
					</m-row>
				</m-col>
			</m-row>
			<m-row style="margin-top: 56px" :gx="6" :gy="12">
				<m-col :span="12">
					<m-field label="Địa chỉ">
						<m-input v-model="employee.Address" @input="handleInput" maxlength="255" />
					</m-field>
				</m-col>
				<m-col :span="3">
					<m-field label="ĐT di động">
						<m-input
							v-model="employee.PhoneNumber"
							name="PhoneNumber"
							@input="handleInput"
							maxlength="30"
						/>
					</m-field>
				</m-col>
				<m-col :span="3">
					<m-field label="ĐT cố định">
						<m-input
							v-model="employee.TelephoneNumber"
							name="TelephoneNumber"
							@input="handleInput"
							maxlength="30"
						/>
					</m-field>
				</m-col>
				<m-col :span="3">
					<m-field label="Email">
						<m-input
							v-model="employee.Email"
							name="Email"
							:error="getInputError('Email')"
							@blur="handleBlur"
							@input="handleInput"
							maxlength="255"
						/>
					</m-field>
				</m-col>
				<m-col :span="3" />
				<m-col :span="3">
					<m-field label="Số tài khoản">
						<m-input
							v-model="employee.BankAccountNumber"
							name="BankAccountNumber"
							@input="handleInput"
							maxlength="50"
						/>
					</m-field>
				</m-col>
				<m-col :span="3">
					<m-field label="Tên ngân hàng">
						<m-input v-model="employee.BankName" @input="handleInput" maxlength="120" />
					</m-field>
				</m-col>
				<m-col :span="3">
					<m-field label="Chi nhánh">
						<m-input v-model="employee.BankBranchName" @input="handleInput" maxlength="255" />
					</m-field>
				</m-col>
			</m-row>
		</form>
		<template #footer>
			<div>
				<m-button color="secondary" @click="$emit('close')">Hủy</m-button>
			</div>
			<div>
				<m-button v-tooltip="'Cất (Ctrl + S)'" color="secondary" @click="handleSave()"
					>Cất</m-button
				>
				&nbsp;
				<m-button v-tooltip="'Cất và thêm (Ctrl + Shift + S)'" @click="handleSaveAndAdd"
					>Cất và thêm</m-button
				>
			</div>
		</template>
		<m-spinner v-if="popupLoading" />
		<m-message-dialog v-bind="messageProps" v-on="messageEvents" />
	</m-popup>
</template>
<script>
import MPopup from "../components/bases/MPopup.vue";
import MButton from "../components/bases/MButton.vue";
import MCheckBox from "../components/bases/MCheckBox.vue";
import MRow from "../components/bases/Grid/MRow.vue";
import MCol from "../components/bases/Grid/MCol.vue";
import MField from "../components/bases/MField.vue";
import MInput from "../components/bases/MInput.vue";
import MDatePicker from "../components/bases/MDatePicker.vue";
import MCombobox from "../components/bases/MCombobox.vue";
import MRadio from "../components/bases/MRadio.vue";
import MSpinner from "../components/bases/MSpinner.vue";
import MMessageDialog from "../components/bases/MMessageDialog.vue";
import ApiConfig from "../api-config";
import useForm from "../composables/useForm";
import useMessageDialog from "../composables/useMessageDialog";
import { isDateLessThan, isEmail, isRequired } from "../utils/validators";
import formatter, { DateFormat } from "../utils/formatter";
import text from "../common/text";

/**
 * @author Createdby: nvchung (16/02/2022)
 */
export default {
	components: {
		MPopup,
		MButton,
		MCheckBox,
		MRow,
		MCol,
		MField,
		MDatePicker,
		MInput,
		MCombobox,
		MRadio,
		MSpinner,
		MMessageDialog,
	},
	emits: ["close", "saveAndAdd"],
	props: {
		/**
		 * id nhân viên
		 */
		employeeId: String,
		/**
		 * Form mode
		 * @values add, clone, add
		 * @default add
		 */
		mode: {
			type: String,
			default: "add",
		},
	},
	/**
	 * @author Createdby: nvchung (16/02/2022)
	 */
	setup() {
		//rule validate đơn vị
		const validateDepartment = (dpmId, emp) => {
			if (dpmId) return true;
			return emp.DepartmentName
				? text.DPM_NOT_EXIST_ERR
				: formatter.formatString(text.REQUIRED_ERR, text.DPM);
		};
		//rule validate mã nhân viên
		const validateEmployeeCode = (code) => {
			return /^NV-\d{1,22}$/.test(code) ? true : text.EMP_CODE_INVALID_ERR;
		};
		//set up form hook
		const { values: employee, ...rest } = useForm({
			EmployeeCode: [
				isRequired(formatter.formatString(text.REQUIRED_ERR, text.EMP_CODE)),
				validateEmployeeCode,
			],
			FullName: [isRequired(formatter.formatString(text.REQUIRED_ERR, text.EMP_FULLNAME))],
			DepartmentId: [validateDepartment],
			DateOfBirth: [
				isDateLessThan(
					formatter.formatString(text.DATE_GREATER_THAN_TODAY_ERR, text.DATE_OF_BIRTH),
					new Date()
				),
			],
			Email: [isEmail(text.EMAIL_ERR)],
			IdentityDate: [
				isDateLessThan(
					formatter.formatString(text.DATE_GREATER_THAN_TODAY_ERR, text.INDENTITY_DATE),
					new Date()
				),
			],
		});
		return { employee, ...rest, ...useMessageDialog() };
	},
	/**
	 * @author Createdby: nvchung (16/02/2022)
	 */
	data() {
		return {
			departments: undefined, //danh sách đơn vị
			departmentHeaders: [
				{ text: text.DPM_CODE, value: "DepartmentCode" },
				{ text: text.DPM_NAME, value: "DepartmentName" },
			], //header của table trong combobox đơn vị
			departmentLoading: false, //combobox loading
			popupLoading: false, //hiện spinner
		};
	},
	inheritAttrs: false,
	/**
	 * @author Createdby: nvchung (16/02/2022)
	 */
	created() {
		switch (this.mode) {
			case "add": //lấy code mới nếu là form add
				this.getNewEmployeeCode();
				break;
			case "edit": //load nhân viên nếu là form sửa
				this.loadEmployee();
				break;
			case "clone": //load nhân viên rồi lấy code mới nếu là form clone
				this.loadEmployee().then(() => this.getNewEmployeeCode());
		}
	},
	/**
	 * @author Createdby: nvchung (16/02/2022)
	 */
	mounted() {
		//bind event phím tắt
		document.addEventListener("keydown", this.handleShortKey);
	},
	/**
	 * @author Createdby: nvchung (16/02/2022)
	 */
	unmounted() {
		//remove event phím tắt
		document.removeEventListener("keydown", this.handleShortKey);
	},
	methods: {
		/**
		 * Xử lý sự kiện phím tắt trên form
		 * @param {KeyboardEvent} evt
		 * @author Createdby: nvchung (16/02/2022)
		 */
		handleShortKey(evt) {
			if (evt.key == "Escape") {
				this.closeForm();
			}
			if (evt.ctrlKey && evt.key.toLowerCase() == "s") {
				evt.preventDefault();
				if (evt.shiftKey) {
					this.handleSaveAndAdd();
				} else {
					this.handleSave();
				}
			}
		},
		/**
		 * Đóng form
		 * @author Createdby: nvchung (16/02/2022)
		 */
		closeForm() {
			if (this.isFormChanged) {
				this.showConfirm({
					text: text.FORM_CHANGE_WARN,
					icon: "question",
					showCancel: true,
					onYes: () => this.handleSave(),
					onNo: () => this.$emit("close"),
				});
			} else {
				this.$emit("close");
			}
		},
		/**
		 * Lấy mã nhân viên mới
		 * @author Createdby: nvchung (16/02/2022)
		 */
		async getNewEmployeeCode() {
			try {
				const { data: code } = await this.$axios.get(ApiConfig.Employee.NEXT_CODE);
				this.employee.EmployeeCode = code;
			} catch (err) {
				this.handleAxiosError(err);
			}
		},
		/**
		 * Hàm lọc của combobox đơn vị
		 * @author Createdby: nvchung (16/02/2022)
		 */
		departmentFilter(department, search) {
			const reg = new RegExp(search, "i");
			return reg.test(department.DepartmentName);
		},
		/**
		 * Load dữ liệu lên combobox đơn vị
		 * @author Createdby: nvchung (16/02/2022)
		 */
		loadDepartments() {
			if (this.departments || this.departmentLoading) return;
			this.departmentLoading = true;
			this.$axios
				.get(ApiConfig.Department.BASE)
				.then(({ data }) => {
					this.departments = data;
				})
				.finally(() => {
					this.departmentLoading = false;
				});
		},
		/**
		 * Chọn một đơn vị từ combobox
		 * @author Createdby: nvchung (16/02/2022)
		 */
		handleDepartmentSelect(department) {
			this.employee.DepartmentId = department?.DepartmentId;
			this.errors.DepartmentId = null;
		},
		/**
		 * Load nhân viên lên form
		 * @author Createdby: nvchung (16/02/2022)
		 */
		async loadEmployee() {
			this.popupLoading = true;
			try {
				const { data: emp } = await this.$axios.get(
					`${ApiConfig.Employee.BASE}/${this.employeeId}`
				);
				//format dữ liệu
				emp.DateOfBirth = this.$formatters.formatDate(emp.DateOfBirth);
				emp.IdentityDate = this.$formatters.formatDate(emp.IdentityDate);
				this.employee = emp;
			} catch (err) {
				this.handleAxiosError(err);
			} finally {
				this.popupLoading = false;
			}
		},
		/**
		 * Lấy key của lỗi đầu tiên
		 * @author Createdby: nvchung (16/02/2022)
		 */
		getFirstErrorKey() {
			return Object.keys(this.errors).find((k) => this.errors[k]);
		},
		/**
		 * Lưu nhân viên
		 * @param {Boolean} preventClose - ngăn đóng form sau khi lưu dữ liệu
		 * @author Createdby: nvchung (16/02/2022)
		 */
		async handleSave(preventClose = false) {
			//validate dữ liệu
			if (!this.validate()) {
				const firstErrKey = this.getFirstErrorKey();
				this.showError({
					text: this.errors[firstErrKey],
					onClose: () => {
						this.$refs.form[firstErrKey]?.focus();
					},
				});
				return;
			}
			//format data
			const data = { ...this.employee };
			data.DateOfBirth = this.$formatters.formatDate(
				data.DateOfBirth,
				DateFormat.YMDH,
				DateFormat.DMY
			);
			data.IdentityDate = this.$formatters.formatDate(
				data.IdentityDate,
				DateFormat.YMDH,
				DateFormat.DMY
			);
			//gọi api
			this.popupLoading = true;
			let hasError = false;
			try {
				if (this.mode == "edit") {
					//form sửa
					await this.$axios.put(`${ApiConfig.Employee.BASE}/${this.employeeId}`, data);
				} else {
					//form thêm/nhân bản
					await this.$axios.post(ApiConfig.Employee.BASE, data);
				}
			} catch (error) {
				hasError = true;
				this.handleAxiosError(error);
			} finally {
				this.popupLoading = false;
			}
			if (!preventClose && !hasError) {
				//nếu cho phép đóng và không có lỗi
				this.$emit("close", 1);
			}
		},
		/**
		 * Cất và thêm
		 * @author Createdby: nvchung (16/02/2022)
		 */
		async handleSaveAndAdd() {
			await this.handleSave(true);
			this.reset();
			this.getNewEmployeeCode();
			this.$emit("saveAndAdd");
		},
		/**
		 * Xử lý lỗi khi request với axios
		 * @author Createdby: nvchung (16/02/2022)
		 */
		handleAxiosError(err) {
			console.log(err.response);
			let userMsg = err.response?.data?.userMessage;
			const errProp = err.response?.data?.data?.ErrorProperty;
			if (!userMsg) {
				userMsg = text.UNKNOWN_ERR;
			}
			this.showError({
				text: userMsg,
				onClose: () => {
					this.$refs.form[errProp]?.focus();
					this.errors[errProp] = userMsg;
				},
			});
		},
	},
};
</script>
