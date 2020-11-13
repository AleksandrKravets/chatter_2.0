import React from 'react';
import classNames from 'classnames';

import styles from './spinner.module.scss';

type Props = {
    isFullPage: boolean
}

const Spinner: React.FC<Props> = ({ isFullPage }) => {
    return (
        <div className={classNames(styles.spinner, {
            [styles['spinner_full-page']]: isFullPage
        })}>
            <div className={styles['spinner__content']}>
                <div className={styles['spinner__double-bounce1']}></div>
                <div className={styles['spinner__double-bounce2']}></div>
            </div>
        </div>
    )
}

export default Spinner;
