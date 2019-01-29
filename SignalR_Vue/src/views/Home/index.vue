<template src="./template.html"></template>
<script>
export default {
  name: "home",
  components: {},
  data() {
    return {
      name: "",
      sendText: "",
      proxy: {},
      message: []
    };
  },
  methods: {
    send() {
      this.proxy.invoke("NewChatMessage", this.name, this.sendText);
    },
    view(time, name, text) {
      console.log(time, name, text);
      this.message.push({
        time: time,
        name: name,
        text: text
      });
    }
  },
  mounted() {
    // //連線訊息
    // let conn = $.hubConnection("http://localhost:53301/");
    // let proxy = conn.createHubProxy("DemoChatHub"); //對應hub

    // //事件監聽
    // proxy.on("sendMessage", (time, name, text) => {
    //   this.view(time, name, text);
    // });

    // this.proxy = proxy;
    // //開始連線
    // conn
    //   .start()
    //   .done(() => {
    //     console.log("連線成功");
    //   })
    //   .fail(() => {
    //     console.log("連線失敗");
    //   });
    if (this.$store.state.SIGNALR_STATE == "-1") {
      this.$router.push({ name: "login" });
    }
  }
};
</script>
<style lang="scss"  src="./template.scss" scoped>
</style>
