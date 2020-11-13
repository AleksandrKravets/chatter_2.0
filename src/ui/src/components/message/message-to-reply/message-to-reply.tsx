import React from 'react'
import classNames from 'classnames';

import styles from './message-to-reply.module.scss';

type Props = {
    text: string
    ownerNickname: string
    isReplyToCurrentUser: boolean
    isCurrentUserMessage: boolean
}

const MessageToReply: React.FC<Props> = ({
    text, 
    ownerNickname, 
    isCurrentUserMessage,
    isReplyToCurrentUser
}) => {
    return <div className={classNames(styles['message-to-reply'], {
        [styles['message-to-reply_border-black']]: isCurrentUserMessage || isReplyToCurrentUser,
        [styles['message-to-reply_border-white']]: !isCurrentUserMessage && !isReplyToCurrentUser
    })}>
        <div className={styles['message-to-reply__owner-nickname']}>
            {
                !isReplyToCurrentUser && <span>{ownerNickname}</span>
            }
        </div>
        <div className={styles['message-to-reply__text']}>
            {text}
        </div>
    </div>
}

export default MessageToReply;