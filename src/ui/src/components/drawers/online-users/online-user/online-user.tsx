import React from 'react';
import { useDispatch } from 'react-redux';
import { fetchMuteUser, fetchUnmuteUser } from '../../../../redux/users-reducer';
import classNames from 'classnames';

import styles from './online-user.module.scss';

type Props = {
    id: number
    nickname: string
    isMuted: boolean
    isCurrentUser: boolean
}

const OnlineUser: React.FC<Props> = ({
    id,
    nickname,
    isMuted,
    isCurrentUser
}) => {
    const dispatch = useDispatch();

    const muteOrUnmuteUser = () => {
        if(isCurrentUser) return
        
        if (isMuted) dispatch(fetchUnmuteUser(id));
        else dispatch(fetchMuteUser(id));
    }

    return (
        <div className={styles['online-user']}>
            <div className={styles['online-user__nickname']}>
                {nickname}
            </div>
            <div className={styles['online-user__options']}>
                <div
                    className={classNames(styles['online-user__option'], {
                        [styles['online-user__option_red']]: !isCurrentUser && isMuted,
                        [styles['online-user__option_green']]: !isCurrentUser && !isMuted,
                        // [styles['online-user__option_yellow']]: isCurrentUser
                    })}
                    onClick={muteOrUnmuteUser}
                >
                    {
                        !isCurrentUser 
                            ? (isMuted
                                ? <i className="fas fa-comment-slash"></i>
                                : <i className="fas fa-comments"></i>)
                            : <i className="far fa-user"></i>
                    }
                </div>
            </div>
        </div>
    )
}

export default OnlineUser;