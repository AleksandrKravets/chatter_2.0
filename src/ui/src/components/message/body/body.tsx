import React from 'react';
import classNames from 'classnames';

import MessageToReply from './../message-to-reply';

import styles from './body.module.scss';

type Props = {
    text: string
    messageToReplyOwnerNickname: string | null | undefined
    messageToReplyText: string | null | undefined
    isReply: boolean
    isCurrentUserMessage: boolean
    isReplyToCurrentUser: boolean
}

const Body: React.FC<Props> = ({ 
    text, 
    messageToReplyOwnerNickname,
    messageToReplyText,
    isReply,
    isCurrentUserMessage,
    isReplyToCurrentUser
}) => {
    return (
        <div className={styles.body}>
            {
                isReply && <MessageToReply
                    ownerNickname={!messageToReplyOwnerNickname ? '' : messageToReplyOwnerNickname}
                    text={!messageToReplyText ? '' : messageToReplyText}
                    isReplyToCurrentUser={isReplyToCurrentUser}
                    isCurrentUserMessage={isCurrentUserMessage}
                />
            }
            <div className={classNames(styles.body__text, {
                [styles.body__text_right]: isCurrentUserMessage,
                [styles.body__text_left]: !isCurrentUserMessage
            })}>
                {text}
            </div>
        </div>
    )
}

export default Body;