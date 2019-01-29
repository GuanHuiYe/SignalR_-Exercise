<template src="./template.html"></template>
<script>
export default {
  data() {
    return {
      name: ""
    };
  },
  computed: {
    signalR_State() {
      if (this.$store.state.SIGNALR_STATE == 0) {
        return true;
      } else {
        return false;
      }
    }
  },
  methods: {
    addUser() {
      this.$store.dispatch("ADD_USER", this.name);
      this.$router.push({ name: "home" });
    }
  },
  mounted() {
    this.$store.dispatch("INIT_SIGNALR");
  }
};
</script>
<style lang="scss"  src="./template.scss" scoped>
</style>
