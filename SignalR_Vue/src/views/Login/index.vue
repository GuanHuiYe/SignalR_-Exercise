<template src="./template.html"></template>
<script>
export default {
  computed: {
    signalR_State() {
      if (this.$store.state.SIGNALR_STATE == 0) {
        return true;
      } else {
        return false;
      }
    }
  },
  mounted() {
    this.$store.dispatch("INIT_SIGNALR");
  }
};
</script>
<style lang="scss"  src="./template.scss" scoped>
</style>
