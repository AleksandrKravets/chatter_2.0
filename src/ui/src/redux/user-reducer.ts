import { authFetch } from '../utils/auth/auth-provider';
import HTTPMethod from '../utils/auth/http-methods';
import { BaseThunkType, InferActionsTypes } from './store';

export type UserType = {
    id: number
    nickname: string
}

const initialState = {
    user: null as unknown as UserType,
    isLoading: false
}

const user = (state = initialState, action: ActionsType): InitialStateType => {
    switch (action.type) {
        case 'USER/SET':
            return {
                ...state,
                user: action.payload
            };
        case 'USER/SET_LOADING':
            return {
                ...state,
                isLoading: action.payload
            };

        default:
            return state;
    }
}

export const actions = {
    setUser: (payload: UserType) => ({
        type: 'USER/SET',
        payload
    } as const),
    setUserLoading: (value: boolean) => ({
        type: 'USER/SET_LOADING',
        payload: value
    } as const)
}

export const fetchUser = (): ThunkType => async (dispatch) => {
    dispatch(actions.setUserLoading(true));
    var result = await authFetch(HTTPMethod.GET, 'http://localhost:5000/api/user-info')
    dispatch(actions.setUser(result.data));
    dispatch(actions.setUserLoading(false));
}

type ActionsType = InferActionsTypes<typeof actions>
type ThunkType = BaseThunkType<ActionsType>

export default user;
export type InitialStateType = typeof initialState