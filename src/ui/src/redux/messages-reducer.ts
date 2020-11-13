import { authFetch } from '../utils/auth/auth-provider';
import HTTPMethod from '../utils/auth/http-methods';
import { BaseThunkType, InferActionsTypes } from './store';

export type MessageOwnerType = {
    id: number
    nickname: string
}

export type MessageToReplyDataType = {
    text: string
    ownerId: number
    ownerNickname: string
}

export type MessageType = {
    id: number
    text: string
    creationTime: string
    isUpdated: boolean
    isReply: boolean
    owner: MessageOwnerType
    messageToReplyData: MessageToReplyDataType | null
}

const initialState = {
    messages: [
        // {
        //     id: 1,
        //     text: 'ok',
        //     creationTime: '11:45',
        //     isUpdated: true,
        //     isReply: false,
        //     owner: { id: 1, nickname: 'Sasha' },
        //     messageToReplyData: null
        // },
        // {
        //     id: 2,
        //     text: 'message with reply from current user',
        //     creationTime: '11:45',
        //     isUpdated: false,
        //     isReply: true,
        //     owner: { id: 1, nickname: 'Sasha' },
        //     messageToReplyData: {
        //         ownerId: 2,
        //         ownerNickname: 'Nick',
        //         text: 'message to reply'
        //     }
        // },
        // {
        //     id: 3,
        //     text: 'message without reply from other user',
        //     creationTime: '11:45',
        //     isUpdated: false,
        //     isReply: false,
        //     owner: { id: 2, nickname: 'Nick' },
        //     messageToReplyData: null
        // },
        // {
        //     id: 4,
        //     text: 'message with reply from other user to current user',
        //     creationTime: '11:45',
        //     isUpdated: false,
        //     isReply: true,
        //     owner: { id: 2, nickname: 'Nick' },
        //     messageToReplyData: {
        //         ownerId: 1,
        //         ownerNickname: 'Alex',
        //         text: 'message to reply'
        //     }
        // },
        // {
        //     id: 5,
        //     text: 'message with reply from other user',
        //     creationTime: '11:45',
        //     isUpdated: false,
        //     isReply: true,
        //     owner: { id: 2, nickname: 'Nick' },
        //     messageToReplyData: {
        //         ownerId: 3,
        //         ownerNickname: 'Oleg',
        //         text: 'message to reply'
        //     }
        // }
    ] as Array<MessageType>,
    minMessageId: null as number | null,
    nextPageExists: true,
    isPrepending: false
}

const messages = (state = initialState, action: ActionsType): InitialStateType => {
    switch (action.type) {
        case 'MESSAGES/DELETE':
            return {
                ...state,
                messages: [...state.messages.filter(x => x.id !== action.payload)]
            };
        case 'MESSAGES/CREATE':
            return {
                ...state,
                messages: [...state.messages, action.payload]
            };
        case 'MESSAGES/UPDATE':
            return {
                ...state,
                messages: state.messages.map(x => {
                    if(x.id === action.payload.id) {
                        return {
                            ...x,
                            text: action.payload.updatedText,
                            isUpdated: true
                        }
                    }
    
                    return x;
                })
            };
        case 'MESSAGES/SET_IS_PREPENDING':
            return {
                ...state,
                isPrepending: action.payload
            };
        case 'MESSAGES/PREPEND':
            return {
                ...state,
                messages: [...action.payload.messages, ...state.messages],
                nextPageExists: action.payload.nextPageExists,
                minMessageId: Math.min(...action.payload.messages.map(x => x.id))
            };
        default:
            return state;
    }
}

export const actions = {
    createMessage: (message: MessageType) => ({
        type: 'MESSAGES/CREATE',
        payload: message
    } as const),
    deleteMessage: (id: number) => ({
        type: 'MESSAGES/DELETE',
        payload: id
    } as const),
    updateMessage: (id: number, updatedText: string) => ({
        type: 'MESSAGES/UPDATE',
        payload: {
            id, updatedText
        }
    } as const),
    setIsPrepending: (value: boolean) => ({
        type: 'MESSAGES/SET_IS_PREPENDING',
        payload: value
    } as const),
    prependMessages: (messages: Array<MessageType>, nextPageExists: boolean) => ({
        type: 'MESSAGES/PREPEND',
        payload: {
            messages,
            nextPageExists
        }
    } as const)
}

export const fetchMessages = (minMessageId: number | null = null): ThunkType => async (dispatch) => {
    dispatch(actions.setIsPrepending(true));

    const result = await authFetch(HTTPMethod.GET, `http://localhost:5000/api/messages?${minMessageId === null ? '' : `LastMessageId=${minMessageId}`}`)

    dispatch(actions.prependMessages(
        result.data.result,
        result.data.nextPageExists
    ));

    dispatch(actions.setIsPrepending(false));
}

export const fetchCreateMessage = (messageText: string): ThunkType => async (dispatch) => {
    await authFetch(HTTPMethod.POST, `http://localhost:5000/api/messages`, { text: messageText })
}

export const fetchUpdateMessage = (id: number, messageText: string): ThunkType => async (dispatch) => {
    await authFetch(HTTPMethod.PUT, `http://localhost:5000/api/messages/${id}`, { text: messageText })
}

export const fetchDeleteMessage = (id: number): ThunkType => async (dispatch) => {
    await authFetch('DELETE', `http://localhost:5000/api/messages/${id}`)
}

export const fetchReply = (messageToReplyId: number, replyText: string): ThunkType => async (dispatch) => {
    await authFetch('POST', `http://localhost:5000/api/messages/${messageToReplyId}/reply`, { text: replyText })
}

type ActionsType = InferActionsTypes<typeof actions>
type ThunkType = BaseThunkType<ActionsType>

export default messages;
export type InitialStateType = typeof initialState
