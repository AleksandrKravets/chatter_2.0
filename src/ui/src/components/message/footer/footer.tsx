import React from 'react';
import classNames from 'classnames';

import styles from './footer.module.scss';

type Props = {
    isUpdated: boolean
}

const Footer: React.FC<Props> = ({
    isUpdated
}) => {
    return (
        <div className={styles.footer}>
            {
                isUpdated && <div className={classNames(styles.footer__label, styles.footer__label_green)}>
                    edited
                </div>
            }

        </div>
    )
}

export default Footer;
