import React from 'react';
import { useDispatch } from 'react-redux';

import { actions, InputMode } from './../../redux/input-reducer';
import { fetchDeleteMessage } from '../../redux/messages-reducer';
import Header from './header';
import Footer from './footer';
import Content from './content';

import styles from './message.module.scss';

type Props = {
    id: number
    text: string
    creationTime: string
    ownerNickname: string
    messageToReplyOwnerNickname: string | null | undefined
    messageToReplyText: string | null | undefined
    isCurrentUserMessage: boolean
    isUpdated: boolean
    isReply: boolean
    isReplyToCurrentUser: boolean
    isMuted: boolean
}

const Message: React.FC<Props> = ({
    id,
    text,
    creationTime,
    ownerNickname,
    messageToReplyOwnerNickname,
    messageToReplyText,
    isCurrentUserMessage,
    isUpdated,
    isReply,
    isReplyToCurrentUser,
    isMuted
}) => {
    const dispatch = useDispatch();

    const onEditBtnClick = () => {
        dispatch(actions.setSelectedMessage(id, InputMode.Edit))
        dispatch(actions.setInputText(text))
    }

    const onDeleteBtnClick = () => {
        dispatch(fetchDeleteMessage(id));
    }

    const onReplyBtnClick = () => {
        dispatch(actions.setSelectedMessage(id, InputMode.Reply))
    }

    return (
        <div className={styles.message}>
            <Header
                isCurrentUserMessage={isCurrentUserMessage}
                ownerNickname={ownerNickname}
            />
            <Content
                id={id}
                creationTime={creationTime}
                text={text}
                isCurrentUserMessage={isCurrentUserMessage}
                isReply={isReply}
                isReplyToCurrentUser={isReplyToCurrentUser}
                messageToReplyOwnerNickname={messageToReplyOwnerNickname}
                messageToReplyText={messageToReplyText}
            />
            <Footer isUpdated={isUpdated} />
        </div>
    )

    return (
        <div className={styles.message}>
            {
                isMuted
                    ? <div>Muted message</div>
                    : <>
                        <div>
                            {
                                isCurrentUserMessage && <>
                                    <button onClick={onEditBtnClick}>Edit</button>
                                    <button onClick={onDeleteBtnClick}>Delete</button>
                                </>
                            }
                            {
                                !isCurrentUserMessage && <>
                                    <button onClick={onReplyBtnClick}>Reply</button>
                                </>
                            }

                        </div>
                        {
                            isReply && <div>Reply</div>
                        }
                        {
                            isReplyToCurrentUser && <div>Reply to current user</div>
                        }
                        {
                            isCurrentUserMessage && <div>Current user message</div>
                        }
                        {
                            !isCurrentUserMessage && <div>Other user message</div>
                        }
                        <div className={styles.header}>
                            {
                                !isCurrentUserMessage && <div>
                                    {ownerNickname}
                                </div>
                            }
                        </div>
                        <div className={styles.body}>
                            {
                                // isReply && <MessageToReply
                                //     // ownerNickname={!messageToReplyOwnerNickname ? '' : messageToReplyOwnerNickname}
                                //     // text={!messageToReplyText ? '' : messageToReplyText}
                                //     isReplyToCurrentUser={isReplyToCurrentUser}
                                //     isCurrentUserMessage={isCurrentUserMessage}
                                // />
                            }
                            <div>
                                {text}
                            </div>
                            <div>
                                {creationTime}
                            </div>
                        </div>
                        <div className={styles.footer}>
                            {
                                isUpdated && <div>
                                    edited
                    </div>
                            }
                        </div>
                    </>
            }

        </div>
    )
}

export default Message;