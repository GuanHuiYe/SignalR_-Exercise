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
      console.log(this.$store.state.PROXY);
      this.$store.state.PROXY.invoke("UpdateUser", this.name);
      this.$store.mutations.SET_USER_NAME(this.name);
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
