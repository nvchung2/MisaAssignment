<template>
	<li :class="['list-item', active && 'active', hovered && 'hovered']" @click="$emit('click')">
		<slot />
	</li>
</template>
<script>
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
