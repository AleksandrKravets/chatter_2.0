import React from 'react';

import styles from './header.module.scss';

type Props = {
    isCurrentUserMessage: boolean
    ownerNickname: string
}

const Header: React.FC<Props> = ({
    isCurrentUserMessage,
    ownerNickname
}) => {
    return <div className={styles.header}>
        {
            !isCurrentUserMessage && <div className={styles["header__owner-nickname"]}>
                {ownerNickname}
            </div>
        }
    </div>
}

export default Header;