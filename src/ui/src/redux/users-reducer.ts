import { authFetch } from '../utils/auth/auth-provider';
import HTTPMethod from '../utils/auth/http-methods';
import { BaseThunkType, InferActionsTypes } from './store';

export type OnlineUserType = {
    id: number
    nickname: string
    isMuted: boolean
    isCurrentUser: boolean
}

export type MutedUserType = {
    id: number
}

const initialState = {
    onlineUsers: [] as Array<OnlineUserType>,
    mutedUsers: [] as Array<MutedUserType>,
    areMutedUsersLoaded: false,
    isLoading: false,
    onlineUsersCount: 0
}

const users = (state = initialState, action: ActionsType): InitialStateType => {
    switch (action.type) {
        case 'USERS/SET-ONLINE-USERS':
            return {
                ...state,
                onlineUsers: action.payload
            };
        case 'USERS/SET_LOADING':
            return {
                ...state,
                isLoading: action.payload
            }
        case 'USERS/SET-ONLINE-USERS-COUNT':
            return {
                ...state,
                onlineUsersCount: action.payload
            }
        case 'USERS/MUTE':
            return {
                ...state,
                onlineUsers: state.onlineUsers.map(x => {
                    if (x.id === action.payload) {
                        return {
                            ...x,
                            isMuted: true
                        }
                    }

                    return x;
                }),
                mutedUsers: [...state.mutedUsers, { id: action.payload }]
            }
        case 'USERS/UNMUTE':
            return {
                ...state,
                onlineUsers: state.onlineUsers.map(x => {
                    if (x.id === action.payload) {
                        return {
                            ...x,
                            isMuted: false
                        }
                    }

                    return x;
                }),
                mutedUsers: state.mutedUsers.filter(x => x.id !== action.payload)
            }
        case 'USERS/SET-MUTED-USERS':
            return {
                ...state,
                mutedUsers: action.payload,
                areMutedUsersLoaded: true
            };

        default:
            return state;
    }
}

export const actions = {
    setOnlineUsers: (users: Array<OnlineUserType>) => ({
        type: 'USERS/SET-ONLINE-USERS',
        payload: users
    } as const),
    setOnlineUsersLoading: (value: boolean) => ({
        type: 'USERS/SET_LOADING',
        payload: value
    } as const),
    setOnlineUsersCount: (value: number) => ({
        type: 'USERS/SET-ONLINE-USERS-COUNT',
        payload: value
    } as const),
    mute: (userId: number) => ({
        type: 'USERS/MUTE',
        payload: userId
    } as const),
    unmute: (userId: number) => ({
        type: 'USERS/UNMUTE',
        payload: userId
    } as const),
    setMutedUsers: (payload: Array<MutedUserType>) => ({
        type: 'USERS/SET-MUTED-USERS',
        payload
    } as const)
}

export const fetchOnlineUsers = (): ThunkType => async (dispatch) => {
    dispatch(actions.setOnlineUsersLoading(true));
    const result = await authFetch(HTTPMethod.GET, 'http://localhost:5000/api/online-users')
    dispatch(actions.setOnlineUsers(result.data));
    dispatch(actions.setOnlineUsersLoading(false));
}

export const fetchOnlineUsersCount = (): ThunkType => async (dispatch) => {
    const result = await authFetch(HTTPMethod.GET, 'http://localhost:5000/api/online-users/count')
    dispatch(actions.setOnlineUsersCount(result.data));
}

export const fetchMutedUsers = (): ThunkType => async (dispatch) => {
    const result = await authFetch(HTTPMethod.GET, 'http://localhost:5000/api/mutes')
    dispatch(actions.setMutedUsers(result.data));
}

export const fetchMuteUser = (id: number): ThunkType => async (dispatch) => {
    await authFetch(HTTPMethod.POST, `http://localhost:5000/api/users/${id}/mute`)
    dispatch(actions.mute(id))
}

export const fetchUnmuteUser = (id: number): ThunkType => async (dispatch) => {
    await authFetch(HTTPMethod.POST, `http://localhost:5000/api/users/${id}/unmute`)
    dispatch(actions.unmute(id))
}

export default users;

export type InitialStateType = typeof initialState
type ActionsType = InferActionsTypes<typeof actions>
type ThunkType = BaseThunkType<ActionsType>