<template src="./template.html"></template>
<script>
import chatroom from "@/components/ChatRoom/index.vue";
export default {
  name: "home",
  components: {
    chatroom
  },
  data() {
    return {
      sendText: "",
      proxy: {},
      message: [],
      chats: []
    };
  },
  computed: {
    name() {
      if (this.$store.state.SIGNALR_STATE != "-1")
        return this.$store.state.USER_NAME;
    },
    onlineList() {
      if (this.$store.state.SIGNALR_STATE != "-1")
        return this.$store.state.ONLINE_LIST;
    }
  },
  methods: {
    sendAll() {
      this.proxy.invoke("SendAllMessage", this.name.Name, this.sendText);
    },
    view(time, name, text) {
      console.log(time, name, text);
      this.message.push({
        time: time,
        name: name,
        text: text
      });
    },
    goChat(Index) {
      this.chats.push(this.onlineList[Index]);
    },
    chatMessage(connectionId, time, name, text) {
      console.log("傳過來的");
      console.log(connectionId, time, name, text);

      let chatIndex = -1;
      for (let Index in this.chats) {
        if (connectionId == this.chats[Index].ConnectionID) {
          chatIndex = Index;
          break;
        }
      }
      if (chatIndex != -1) {
        this.$refs.chatRoom[chatIndex].viewMessage(time, name, text);
      } else {
        chatIndex = -1;
        for (let Index in this.onlineList) {
          if (connectionId == this.onlineList[Index].ConnectionID) {
            chatIndex = Index;
            break;
          }
        }
        if (chatIndex != -1) {
          this.chats.push(this.onlineList[chatIndex]);
          this.$nextTick(() => {
            console.log(this.$refs.chatRoom);
            this.chatMessage(connectionId, time, name, text);
          });
        }
      }
    }
  },
  mounted() {
    if (this.$store.state.SIGNALR_STATE == "-1") {
      this.$router.push({ name: "login" });
    } else {
      this.proxy = this.$store.state.PROXY;
      this.proxy.on("sendAllMessage", (time, name, text) => {
        this.view(time, name, text);
      });
      this.proxy.on("SendUserMessage", (connectionId, time, name, text) => {
        this.chatMessage(connectionId, time, name, text);
      });
    }
  }
};
</script>
<style lang="scss"  src="./template.scss" scoped>
</style>
