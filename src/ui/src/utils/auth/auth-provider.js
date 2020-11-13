import createTokenProvider from "./token-provider";
import axios from 'axios';
import { useEffect, useState } from "react";

export const createAuthProvider = () => {
    const tokenProvider = createTokenProvider();

    const login = (newTokens) => {
        tokenProvider.setToken(newTokens);
    };

    const logout = () => {
        tokenProvider.setToken(null);
    };

    const getToken = () => {
        return tokenProvider.getToken();
    }

    const authFetch = async (method, url, payload) => {
        const token = await tokenProvider.getToken();
        return axios({
            method: method,
            url: url,
            data: payload,
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
    }

    const useAuth = () => {
        const [isLogged, setIsLogged] = useState(tokenProvider.isLoggedIn());

        useEffect(() => {
            const listener = (newIsLogged) => {
                setIsLogged(newIsLogged);
            };

            tokenProvider.subscribe(listener);
            return () => {
                tokenProvider.unsubscribe(listener);
            };
        }, []);

        return isLogged;
    };

    return {
        useAuth,
        authFetch,
        login,
        logout,
        getToken
    }
};

const {
    useAuth,
    authFetch,
    login,
    logout,
    getToken
} = createAuthProvider();

export {
    useAuth,
    authFetch,
    login,
    logout,
    getToken
}