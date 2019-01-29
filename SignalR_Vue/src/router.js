import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home/index.vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    //登入
    {
      path: "/login",
      name: "login",
      component: () =>
        import(/* webpackChunkName: "login" */ "./views/Login/index.vue")
    }
  ]
});
