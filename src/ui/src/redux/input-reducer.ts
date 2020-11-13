import { InferActionsTypes } from './store';

export enum InputMode {
    None = 0,
    Edit = 1,
    Create = 2,
    Reply = 3
}

const initialState = {
    inputText: '',
    isMessageSelected: false,
    selectedMessageId: null as number | null,
    mode: InputMode.Create
}

const input = (state = initialState, action: ActionsType): InitialStateType => {
    switch (action.type) {
        case 'INPUT/SET_TEXT':
            return {
                ...state,
                inputText: action.payload
            }
        case 'INPUT/SET_SELECTED_MESSAGE':
            return {
                ...state,
                isMessageSelected: true,
                selectedMessageId: action.payload.messageId,
                mode: action.payload.mode
            }
        case 'INPUT/UNSET_SELECTED_MESSAGE':
            return {
                ...state,
                isMessageSelected: false,
                selectedMessageId: null,
                mode: InputMode.Create
            }
        default:
            return state;
    }
}

export const actions = {
    setInputText: (text: string) => ({
        type: 'INPUT/SET_TEXT',
        payload: text
    } as const),
    setSelectedMessage: (messageId: number, mode: InputMode) => ({
        type: 'INPUT/SET_SELECTED_MESSAGE',
        payload: { messageId, mode }
    } as const),
    unsetSelectedMessage: () => ({
        type: 'INPUT/UNSET_SELECTED_MESSAGE'
    } as const)
}

export default input;

type ActionsType = InferActionsTypes<typeof actions>

export type InitialStateType = typeof initialState