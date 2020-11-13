import React from 'react';

import styles from './typing-users.module.scss';

type Props = {
    isTyping: boolean
    nicknames: Array<string>
}

const TypingUsers: React.FC<Props> = ({ isTyping, nicknames }) => {
    const postfix = nicknames.length > 1 ? 'are typing.' : 'is typing.'
    const users = nicknames.length > 1 ? nicknames.join(', ') : nicknames.join();

    return <>
        {
            isTyping && <div className={styles['typing-users']}>
                <div className={styles['typing-users__users']}>
                    {users}
                </div>
                &nbsp;
                <div className={styles['typing-users__postfix']}>
                    {postfix}
                </div>
            </div>
        }
    </>
}

export default TypingUsers;
