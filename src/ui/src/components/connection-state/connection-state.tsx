import React from 'react';

import styles from './connection-state.module.scss';

type Props = {
    isConnected: boolean
}

const ConnectionState: React.FC<Props> = ({ isConnected }) => {
    return <div className={styles['connection-state']}>
        {
            !isConnected && <>connecting...</>
        }
    </div>
}

export default ConnectionState;