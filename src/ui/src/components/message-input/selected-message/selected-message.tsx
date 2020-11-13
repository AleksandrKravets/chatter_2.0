import React from 'react';
import classNames from 'classnames';

import styles from './selected-message.module.scss';

type Props = {
    isEditMode: boolean
    isReplyMode: boolean
    onCancel: () => void
    text: string
}

const SelectedMessage: React.FC<Props> = ({ isEditMode, isReplyMode, onCancel, text }) => {
    return <div className={styles['selected-message']}>
        <div className={styles['selected-message__content']}>
            <div className={styles['selected-message__type-icon']}>
                {
                    isEditMode && <i className="fas fa-pen"></i>
                }
                {
                    isReplyMode && <i className="fas fa-reply"></i>
                }
            </div>
            <div className={styles['selected-message__text']}>
                {text}
            </div>
        </div>
        <div className={styles['selected-message__options']}>
            <div onClick={onCancel} className={classNames(styles['selected-message__option'], styles['selected-message__option-cancel'])}>
                <i className="fas fa-times"></i>
            </div>
        </div>
    </div>
}

export default SelectedMessage;