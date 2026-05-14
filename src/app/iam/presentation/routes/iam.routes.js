import LoginPage from "../pages/login/login-page.component.vue";
import RegisterPage from "../pages/register/register-page.component.vue";
import ProfilePage from "../pages/profile/profile-page.component.vue";
import RecoverPasswordPage from "../pages/recover-password/recover-password-page.component.vue";

export const iamRoutes = [
    {
        path: "/login",
        name: "login",
        component: LoginPage,
        meta: {
            public: true,
            hideNavigation: true,
        },
    },
    {
        path: "/register",
        name: "register",
        component: RegisterPage,
        meta: {
            public: true,
            hideNavigation: true,
        },
    },
    {
        path: "/recover-password",
        name: "recover-password",
        component: RecoverPasswordPage,
        meta: {
            public: true,
            hideNavigation: true,
        },
    },
    {
        path: "/profile",
        name: "profile",
        component: ProfilePage,
        meta: {
            requiresAuth: true,
        },
    },
];