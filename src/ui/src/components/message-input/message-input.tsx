import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import TextArea from 'antd/lib/input/TextArea';

import SelectedMessage from './selected-message';
import { AppStateType } from '../../redux/store';
import { actions, InputMode } from './../../redux/input-reducer';
import { fetchCreateMessage, fetchReply, fetchUpdateMessage } from '../../redux/messages-reducer';

import styles from './message-input.module.scss';
import { Button } from 'antd';

type Props = {
    onTyping: () => void
}

const MessageInput: React.FC<Props> = ({ onTyping }) => {
    const dispatch = useDispatch();

    const inputText = useSelector((state: AppStateType) => state.input.inputText);
    const isMessageSelected = useSelector((state: AppStateType) => state.input.isMessageSelected);
    const selectedMessageId = useSelector((state: AppStateType) => state.input.selectedMessageId);
    const mode = useSelector((state: AppStateType) => state.input.mode);
    const messages = useSelector((state: AppStateType) => state.messages.messages);

    let selectedMessageText: string = '';

    if (isMessageSelected) {
        selectedMessageText = messages.find(x => x.id === selectedMessageId)!.text;
    }

    const onChange = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
        onTyping();
        dispatch(actions.setInputText(event.target.value));
    }

    const onOperationCancel = () => {
        dispatch(actions.unsetSelectedMessage());
        dispatch(actions.setInputText(''));
    }

    const onBtnClick = () => {
        if(inputText.length === 0) {
            return
        }
        
        if (mode === InputMode.Edit) {
            dispatch(fetchUpdateMessage(selectedMessageId as number, inputText));
        }
        else if (mode === InputMode.Reply) {
            dispatch(fetchReply(selectedMessageId as number, inputText));
        }
        else if (mode === InputMode.Create) {
            dispatch(fetchCreateMessage(inputText));
        }

        onOperationCancel()
    }

    return (
        <div className={styles['message-input']}>

            <div className={styles['message-input__selected-message']}>
                {
                    isMessageSelected && <SelectedMessage
                        isEditMode={mode === InputMode.Edit}
                        isReplyMode={mode === InputMode.Reply}
                        onCancel={onOperationCancel}
                        text={selectedMessageText}
                    />
                }
            </div>

            <div className={styles['message-input__content']}>
                <div className={styles['message-input__input']}>
                    <TextArea
                        value={inputText}
                        onChange={onChange}
                        placeholder="input a message"
                        autoSize={{ minRows: 3, maxRows: 5 }}
                    />
                </div>
                <div className={styles['message-input__button']}>
                    <Button type="primary" shape="circle" size="large" onClick={onBtnClick}>
                        <span className={styles.button}>
                            {
                                mode === InputMode.Edit
                                    ? <i className="fas fa-pen"></i>
                                    : <i className="far fa-paper-plane"></i>
                            }
                        </span>
                    </Button>
                </div>
            </div>

        </div>
    )
}

export default MessageInput;