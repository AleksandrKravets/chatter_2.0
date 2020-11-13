import {
    Action, 
    applyMiddleware, 
    combineReducers, 
    compose, 
    createStore
} from "redux";
import thunkMiddleware, { ThunkAction } from "redux-thunk";

import messagesReducer from './messages-reducer';
import usersReducer from './users-reducer';
import inputReducer from './input-reducer';
import userReducer from './user-reducer';

let rootReducer = combineReducers({
    messages: messagesReducer,
    users: usersReducer,
    input: inputReducer,
    user: userReducer
})

type RootReducerType = typeof rootReducer;

export type AppStateType = ReturnType<RootReducerType>
export type InferActionsTypes<T> = T extends { [keys: string]: (...args: any[]) => infer U } ? U : never
export type BaseThunkType<A extends Action = Action, R = Promise<void>> = ThunkAction<R, AppStateType, unknown, A>

// @ts-ignore
const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const store = createStore(rootReducer, composeEnhancers(applyMiddleware(thunkMiddleware)));

// @ts-ignore
window.__store__ = store;

export default store;
