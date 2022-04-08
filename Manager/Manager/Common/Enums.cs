namespace Manager.Common
{
    public enum Comment
    {
        /// <summary>
        /// Cho phép bình luận
        /// </summary>
        isComment=1,
        /// <summary>
        /// Không cho phép bình luận
        /// </summary>
        notComment=0
    }
    public enum ShowHome
    {
        /// <summary>
        /// Hiện thị trên trang chủ
        /// </summary>
        isShowHome=1,
        /// <summary>
        /// Không hiện thị trên trang chủ
        /// </summary>
        notShowHome=0
    }
    public enum OrderStatus
    {
        /// <summary>
        /// Chờ xác nhận
        /// </summary>
        InProgress,
        /// <summary>
        /// Đã xác nhận chờ lấy hàng
        /// </summary>
        Confirmed,
        /// <summary>
        /// Đang giao
        /// </summary>
        Shipping,
        /// <summary>
        /// Thành công
        /// </summary>
        Success,
        /// <summary>
        /// Hủy đơn
        /// </summary>
        Canceled
    }
}
