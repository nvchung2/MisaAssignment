<template>
	<layout>
		<div class="title">
			<h4>Nhân viên</h4>
			<m-button @click="handleAdd">Thêm mới nhân viên</m-button>
		</div>
		<div class="main-content">
			<div class="flex justify-between mb-16">
				<m-dropdown style="z-index: 103" :contentFull="false">
					<template #trigger>
						<m-button
							@click="handleOpenBulkActions"
							pill
							outlined
							color="secondary"
							class="flex items-center"
							>Thực hiện hàng loạt &nbsp;<m-icon icon="arrow-down-black" :size="16"
						/></m-button>
					</template>
					<m-list>
						<m-list-item @click="handleBulkDelete">Xóa</m-list-item>
					</m-list>
				</m-dropdown>
				<div class="flex items-center gap-10">
					<m-input v-model="searchText" placeholder="Tìm theo mã, tên nhân viên" icon="search" />
					<m-icon v-tooltip="'Lấy lại dữ liệu'" icon="refresh" @click="handleResetData" />
					<m-icon v-tooltip="'Xuất ra excel'" icon="excel" @click="handleExportExcel" />
					<m-icon
						v-tooltip="'Tùy chỉnh giao diện'"
						icon="settings"
						@click="showCustomizerPopup = true"
					/>
				</div>
			</div>
			<m-table
				:headers="selectedEmployeeHeaders"
				:items="employees"
				keyName="EmployeeId"
				style="max-height: 440px"
				v-model="selectedEmployeeIds"
			>
				<template #actions="{ item }">
					<m-button variant="text" color="blue" size="sm" @click="editEmp(item.EmployeeId)"
						>Sửa</m-button
					>
					<m-dropdown appendToBody>
						<template #trigger="{ active }">
							<m-button variant="icon" size="sm">
								<m-icon
									icon="arrow-down-blue"
									:size="16"
									:style="active && 'border: 1px solid #0075c2;'"
								/>
							</m-button>
						</template>
						<m-list>
							<m-list-item @click="clone(item.EmployeeId)">Nhân bản</m-list-item>
							<m-list-item @click="delEmp(item)">Xóa</m-list-item>
							<m-list-item>Ngừng sử dụng</m-list-item>
						</m-list>
					</m-dropdown>
				</template>
			</m-table>
			<div class="flex flex-col items-center mt-16" v-if="employees.length == 0">
				<img src="../assets/imgs/no-content.svg" alt="no-content" />
				<p class="mt-16">Không có dữ liệu</p>
			</div>
			<div class="paging" v-if="employees.length > 0">
				<div>
					Tổng số: <b>{{ employeeCount }}</b> bản ghi
				</div>
				<div class="flex items-center">
					<m-combo-box
						:items="PERPAGE_OPTIONS"
						labelMember="text"
						readonly
						:initialSelected="perPage"
						@select="handlePerPageChange"
						valueMember="value"
						top
						style="z-index: 103"
					/>
					<m-pagination :itemCount="employeeCount" :perPage="perPage.value" v-model="page" />
				</div>
			</div>
		</div>
	</layout>
	<m-spinner v-if="appLoading" />
	<employee-popup
		@close="handlePopupClose"
		v-if="showPopup"
		:employeeId="currentEmployeeId"
		:mode="popupMode"
		@saveAndAdd="handleSaveAndAdd"
	/>
	<m-message-dialog v-bind="messageProps" v-on="messageEvents" />
	<m-popup @close="showCustomizerPopup = false" v-if="showCustomizerPopup">
		<template #title>
			<h2>Tùy chỉnh giao diện</h2>
		</template>
		<m-table
			:headers="CUSTOMIZER_TABLE_HEADERS"
			:items="EMPLOYEE_HEADERS"
			keyName="propName"
			v-model="selectedEmployeePropNames"
			style="max-height: 440px"
		/>
		<template #footer>
			<m-button color="secondary" @click="showCustomizerPopup = false">Hủy</m-button>
			<div>
				<m-button color="secondary" @click="handleResetCustomizer">Lấy mẫu ngầm định</m-button
				>&nbsp;
				<m-button @click="handleSaveCustomizer">Cất</m-button>
			</div>
		</template>
	</m-popup>
	<m-toast-message v-bind="toastProps" v-on="toastEvents" />
</template>
<script>
import Layout from "../components/layouts/Layout.vue";
import MButton from "../components/bases/MButton.vue";
import MInput from "../components/bases/MInput.vue";
import MIcon from "../components/bases/MIcon.vue";
import MTable from "../components/bases/MTable.vue";
import MDropdown from "../components/bases/MDropdown.vue";
import MList from "../components/bases/List/MList.vue";
import MListItem from "../components/bases/List/MListItem.vue";
import MComboBox from "../components/bases/MCombobox.vue";
import MPagination from "../components/bases/MPagination.vue";
import MSpinner from "../components/bases/MSpinner.vue";
import EmployeePopup from "./EmployeePopup.vue";
import MMessageDialog from "../components/bases/MMessageDialog.vue";
import MToastMessage from "../components/bases/MToastMessage.vue";
import MPopup from "../components/bases/MPopup.vue";
import ApiConfig from "../api-config";
import useMessageDialog from "../composables/useMessageDialog";
import useToastMessage from "../composables/useToastMessage";
export default {
	components: {
		Layout,
		MButton,
		MInput,
		MTable,
		MDropdown,
		MIcon,
		MList,
		MListItem,
		MComboBox,
		MPagination,
		MSpinner,
		EmployeePopup,
		MMessageDialog,
		MPopup,
		MToastMessage,
	},
	setup() {
		// Tât cả các headers của bảng nhân viên
		/**
		 * @typedef {Object} header
		 * @property {string} text - Text hiển thị trên table header
		 * @property {string} propName - Tên thuộc tính
		 * @property {Number} width - Độ rộng cột
		 * @property {string} format - Kiểu format dữ liệu
		 */
		/**
		 * @type {Array.<header>} EMPLOYEE_HEADERS
		 */
		const EMPLOYEE_HEADERS = [
			{ text: "Mã nhân viên", propName: "EmployeeCode", width: 120 },
			{ text: "Tên nhân viên", propName: "FullName", width: 174 },
			{
				text: "Giới tính",
				propName: "Gender",
				width: 90,
				format: "gender",
			},
			{
				text: "Ngày sinh",
				propName: "DateOfBirth",
				format: "date",
				width: 110,
			},
			{ text: "Chức danh", propName: "EmployeePosition", width: 150 },
			{ text: "Số CMND", propName: "IdentityNumber", width: 150 },
			{
				text: "Ngày cấp",
				propName: "IdentityDate",
				width: 110,
				format: "date",
			},
			{ text: "Nơi cấp", propName: "IdentityPlace", width: 140 },
			{ text: "Số tài khoản", propName: "BankAccountNumber", width: 150 },
			{ text: "Tên ngân hàng", propName: "BankName", width: 200 },
			{
				text: "Chi nhánh TK ngân hàng",
				propName: "BankBranchName",
				width: 200,
			},
			{ text: "Địa chỉ", propName: "Address", width: 200 },
			{ text: "ĐT di động", propName: "PhoneNumber", width: 150 },
			{ text: "ĐT cố định", propName: "TelephoneNumber", width: 150 },
			{ text: "Email", propName: "Email", width: 200 },
			{ text: "Mã đơn vị", propName: "DepartmentCode", width: 150 },
			{ text: "Tên đơn vị", propName: "DepartmentName", width: 200 },
			{ text: "Ngày tạo", propName: "CreatedDate", format: "date", width: 150 },
			{ text: "Người tạo", propName: "CreatedBy", width: 150 },
			{
				text: "Ngày sửa",
				propName: "ModifiedDate",
				format: "date",
				width: 150,
			},
			{ text: "Người sửa", propName: "ModifiedBy", width: 150 },
		];
		//Header của bảng tùy chỉnh giao diện
		const CUSTOMIZER_TABLE_HEADERS = [{ text: "Tên cột dữ liệu", propName: "text" }];
		//Header mặc định của bảng nhân viên
		const DEFAULT_EMPLOYEE_HEADERS = EMPLOYEE_HEADERS.slice(0, 8);
		//Các lựa chọn của combobox số bản ghi/trang
		const PERPAGE_OPTIONS = [
			{ text: "10 bản ghi trên 1 trang", value: 10 },
			{ text: "20 bản ghi trên 1 trang", value: 20 },
			{ text: "30 bản ghi trên 1 trang", value: 30 },
			{ text: "50 bản ghi trên 1 trang", value: 50 },
			{ text: "100 bản ghi trên 1 trang", value: 100 },
		];
		return {
			EMPLOYEE_HEADERS,
			CUSTOMIZER_TABLE_HEADERS,
			DEFAULT_EMPLOYEE_HEADERS,
			PERPAGE_OPTIONS,
			...useMessageDialog(),
			...useToastMessage(),
		};
	},
	created() {
		this.loadEmployees(); //load dữ liệu lên bảng
	},
	methods: {
		/**
		 * Thêm mới nhân viên
		 */
		handleAdd() {
			this.currentEmployeeId = undefined;
			this.popupMode = "add";
			this.showPopup = true;
		},
		/**
		 * Xóa nhiều nhân viên
		 */
		handleBulkDelete() {
			this.showConfirm({
				text: `Bạn có thực sự muốn xóa ${this.selectedEmployeeIds.length} nhân viên đã chọn không?`,
				icon: "warning",
				onYes: async () => {
					this.appLoading = true;
					try {
						const resp = await this.$axios.post(ApiConfig.Employee.BULK, this.selectedEmployeeIds);
						this.selectedEmployeeIds = [];
						this.makeToast(`${resp.data} bản ghi đã bị xóa`);
						await this.loadEmployees();
					} catch {
						this.showUnknowError();
					} finally {
						this.appLoading = false;
					}
				},
			});
		},
		/**
		 * Export file excel
		 */
		handleExportExcel() {
			window.open(ApiConfig.Employee.EXCEL_FILE);
		},
		/**
		 * Hiện dropdown xóa nhiều
		 */
		handleOpenBulkActions(evt) {
			if (this.selectedEmployeeIds.length == 0) {
				evt.stopPropagation();
			}
		},
		/**
		 * Refresh data
		 */
		handleResetData() {
			this.searchText = "";
			this.page = 1;
			this.perPage = this.PERPAGE_OPTIONS[0];
		},
		/**
		 * Reset tùy chỉnh giao diện về mặc định
		 */
		handleResetCustomizer() {
			this.selectedEmployeePropNames = this.DEFAULT_EMPLOYEE_HEADERS.map(
				({ propName }) => propName
			);
		},
		/**
		 * Lưu lại tùy chỉnh giao diện
		 */
		handleSaveCustomizer() {
			this.selectedEmployeeHeaders = this.EMPLOYEE_HEADERS.filter(({ propName }) =>
				this.selectedEmployeePropNames.includes(propName)
			); //lọc ra các header được chọn
			this.showCustomizerPopup = false;
		},
		/**
		 * Đóng popup thông tin nhân viên
		 * @param {Number} status - Trạng thái trả về từ popup thông tin nhân viên
		 */
		handlePopupClose(status) {
			this.showPopup = false;
			if (status == 1) {
				switch (this.popupMode) {
					case "edit":
						this.makeToast("Dữ liệu đã được cập nhật");
						break;
					case "add":
						this.makeToast("Thêm mới thành công");
						break;
					case "clone":
						this.makeToast("Nhân bản thành công");
				}
				this.loadEmployees();
			}
		},
		//Cất và thêm
		handleSaveAndAdd() {
			this.currentEmployeeId = undefined;
			this.popupMode = "add";
			this.loadEmployees();
		},
		/**
		 * @param {String} search - search keyword
		 * Load nhân viên với phân trang và tìm kiếm
		 */
		async loadEmployees() {
			this.appLoading = true;
			try {
				const { data } = await this.$axios.get(ApiConfig.Employee.FILTER, {
					params: {
						pageSize: this.perPage.value, //số bản ghi/trang
						pageNumber: this.page, //trang lấy
						search: this.searchText, //tìm kiếm
					},
				});
				this.employees = data.Data;
				this.pageCount = data.TotalPages; //tổng số trang
				this.employeeCount = data.TotalRecords; //tổng số bản ghi
			} catch {
				this.showUnknowError();
			} finally {
				this.appLoading = false;
			}
		},
		/**
		 * Sửa nhân viên
		 */
		editEmp(empId) {
			this.popupMode = "edit";
			this.currentEmployeeId = empId;
			this.showPopup = true;
		},
		/**
		 * Nhân bản nhân viên
		 */
		clone(empId) {
			this.popupMode = "clone";
			this.currentEmployeeId = empId;
			this.showPopup = true;
		},
		/**
		 * Xóa nhân viên
		 */
		async delEmp(emp) {
			this.showConfirm({
				text: `Bạn có thực sự muốn xóa nhân viên ${emp.EmployeeCode} không?`,
				icon: "warning",
				onYes: async () => {
					this.appLoading = true;
					try {
						await this.$axios.delete(`${ApiConfig.Employee.BASE}/${emp.EmployeeId}`);
						this.makeToast("Bản ghi đã bị xóa");
						this.loadEmployees();
					} catch {
						this.showUnknowError();
					} finally {
						this.appLoading = false;
					}
				},
			});
		},
		/**
		 * Xử lý sự kiện số bản ghi/trang thay đổi
		 */
		handlePerPageChange(perPage) {
			this.perPage = perPage;
			if (this.page == 1) {
				this.loadEmployees();
			}
			this.page = 1;
		},
		/**
		 * Hiện lỗi server
		 */
		showUnknowError() {
			this.showError({
				text: "Có lỗi xảy ra. Vui lòng liên hệ Misa để được hỗ trợ.",
			});
		},
	},
	data() {
		return {
			searchText: "", //text tìm kiếm
			selectedEmployeeHeaders: this.DEFAULT_EMPLOYEE_HEADERS, //các header được chọn hiển thị bảng nhân viên
			employees: [], //data của bảng nhân viên
			currentEmployeeId: undefined, // id nhân viên được chọn để sửa|nhân bản
			popupMode: "add", //dạng của popup thông tin nhân viên add=thêm, edit=sửa, clone=nhân bản
			perPage: this.PERPAGE_OPTIONS[0], //số bản ghi/trang
			page: 1, //số trang hiện tại
			pageCount: 0, //tổng số trang
			employeeCount: 0, //tổng số bản ghi
			showPopup: false, //hiện popup thông tin nhân viên
			appLoading: false, //hiện spinner
			showCustomizerPopup: false, //hiện popup tùy chỉnh giao diện
			selectedEmployeePropNames: this.DEFAULT_EMPLOYEE_HEADERS.map(({ propName }) => propName), //các thuộc tính được chọn trên popup tùy chỉnh giao diện
			selectedEmployeeIds: [], //mảng id được chọn để xóa nhiều
		};
	},
	watch: {
		/**
		 * Cập nhật lại số trang và load lại dữ liệu khi tìm kiếm thay đổi
		 */
		searchText() {
			this.page = 1;
			this.loadEmployees();
		},
		/**
		 * Load lại nhân viên nếu số trang thay đổi
		 */
		page() {
			this.loadEmployees();
		},
	},
};
</script>
<style scoped src="../styles/views/employee.css"></style>
