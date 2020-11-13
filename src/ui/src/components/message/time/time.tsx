import React from 'react';

import { formatAMPM } from '../../../utils';

import styles from './time.module.scss';

type Props = {
    creationTime: string
}

const Time: React.FC<Props> = ({
    creationTime
}) => {
    return (
        <div className={styles.time}>
            {formatAMPM(new Date(creationTime))}
        </div>
    )
}

export default Time;