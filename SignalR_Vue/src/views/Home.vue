<template>
  <div>
    <p>名字</p>
    <input type="text" v-model="name">
    <p>訊息</p>
    <input type="text" v-model="sendText">
    <button @click="send()">發送</button>
    <div>訊息框：
      <div :key="id" v-for="(mes,id) in message">[{{mes.time}}]{{mes.name}}. {{mes.text}}</div>
    </div>
  </div>
</template>

<script>
// @ is an alias to /src

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
    //連線訊息
    let conn = $.hubConnection("http://localhost/signalr");
    let proxy = conn.createHubProxy("DemoChatHub"); //對應hub

    //事件監聽
    proxy.on("sendMessage", (time, name, text) => {
      this.view(time, name, text);
    });
    this.proxy = proxy;
    //開始連線
    conn
      .start()
      .done(() => {
        console.log("連線成功");
      })
      .fail(() => {
        console.log("連線失敗");
      });
  }
};
</script>
