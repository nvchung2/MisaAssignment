<template>
	<li :class="['list-item', active && 'active', hovered && 'hovered']" @click="$emit('click')">
		<slot />
	</li>
</template>
<script>
/**
 * @author Createdby: nvchung (16/02/2022)
 */
export default {
	props: {
		/**
		 * Trạng thái active
		 */
		active: Boolean,
		/**
		 * Trạng thái hovered
		 */
		hovered: Boolean,
	},
	emits: ["click"],
};
</script>
<style scoped src="../../../styles/components/bases/list.css"></style>
