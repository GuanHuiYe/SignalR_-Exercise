import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    USER_NAME: "",
    PROXY: {},
    SIGNALR_STATE: -1
  },
  mutations: {
    SET_PROXY(state, proxy) {
      state.PROXY = proxy;
    },
    SET_SIGNALR_STATE(state, stateNum) {
      state.SIGNALR_STATE = stateNum;
    },
    SET_USER_NAME(state, name) {
      state.USER_NAME = name;
    }
  },
  actions: {
    //初始化使用者資訊
    INIT_SIGNALR({ commit }) {
      // //load
      // commit("SET_USERINFO", UserInfoLS.Load());
      //連線訊息
      let conn = $.hubConnection("http://localhost:8012/");
      let proxy = conn.createHubProxy("DemoChatHub"); //對應hub
      commit("SET_PROXY", proxy);
      //開始連線
      conn
        .start()
        .done(() => {
          commit("SET_SIGNALR_STATE", 0);
        })
        .fail(() => {
          commit("SET_SIGNALR_STATE", 1);
        });
    },
    ADD_USER({ state, commit }, name) {
      state.PROXY.invoke("UpdateUser", name);
      commit("SET_USER_NAME", name);
    }
  }
});
