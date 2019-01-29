<template src="./template.html"></template>
<script>
export default {
  props: {
    toUser: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      sendText: "",
      message: []
    };
  },
  methods: {
    viewMessage(time, name, text) {
      this.message.push({
        time: time,
        name: name,
        text: text
      });
    },
    sendUser() {
      this.$store.state.PROXY.invoke(
        "SendUserMessage",
        this.toUser.ConnectionID,
        this.$store.state.USER_NAME.Name,
        this.sendText
      );
    }
  }
};
</script>
<style lang="scss"  src="./template.scss" scoped>
</style>
