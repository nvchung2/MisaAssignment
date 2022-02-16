<template>
	<div :class="['top-header', sideBarMini && 'top-header--sidebar-mini']">
		<div class="top-header__left">
			<m-icon icon="bars-black" class="top-header__toggle" @click="onToggleSideBar" />
			<h5>CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC</h5>
		</div>
		<div class="top-header__right">
			<m-icon icon="bell" anim="shake" />
			<div class="avatar">C</div>
			<h5>Ngô Việt Chung</h5>
			<m-icon icon="chevron-down" :size="16" />
		</div>
	</div>
</template>
<script>
import MIcon from "../bases/MIcon.vue";
/**
 * @author Createdby: nvchung (16/02/2022)
 */
export default {
	components: {
		MIcon,
	},
	props: {
		sideBarMini: {
			type: Boolean,
			default: false,
		},
		onToggleSideBar: {
			type: Function,
			required: true,
		},
	},
};
</script>
<style scoped src="../../styles/components/layouts/topheader.css"></style>
