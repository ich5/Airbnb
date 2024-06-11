-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jun 01, 2024 at 12:30 PM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `airbnb`
--

-- --------------------------------------------------------

--
-- Table structure for table `amenities`
--

CREATE TABLE `amenities` (
  `amenities_id` int(11) NOT NULL,
  `type_of_amenities` varchar(255) NOT NULL,
  `description` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `amenities`
--

INSERT INTO `amenities` (`amenities_id`, `type_of_amenities`, `description`) VALUES
(1, 'WiFi', 'Wireless Internet'),
(2, 'Pool', 'Swimming pool'),
(3, 'Parking', 'Free parking on premises'),
(4, 'AC', 'Air conditioning'),
(5, 'Heating', 'Indoor heating'),
(6, 'Kitchen', 'Fully equipped kitchen'),
(7, 'TV', 'Television with cable'),
(8, 'Gym', 'Access to gym'),
(9, 'Hot Tub', 'Hot tub'),
(10, 'Washer', 'Laundry washer');

-- --------------------------------------------------------

--
-- Table structure for table `booking`
--

CREATE TABLE `booking` (
  `booking_id` bigint(255) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `booking_date` datetime DEFAULT NULL,
  `arrival_date` datetime DEFAULT NULL,
  `departure_date` datetime DEFAULT NULL,
  `no_of_guests` int(11) DEFAULT NULL,
  `campingspace_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `booking`
--

INSERT INTO `booking` (`booking_id`, `user_id`, `booking_date`, `arrival_date`, `departure_date`, `no_of_guests`, `campingspace_id`) VALUES
(3, 3, '2024-07-01 00:00:00', '2024-08-01 00:00:00', '2024-08-10 00:00:00', 3, 3),
(4, 4, '2024-08-12 00:00:00', '2024-09-01 00:00:00', '2024-09-11 00:00:00', 2, 2),
(6, 6, '2024-10-25 00:00:00', '2024-11-01 00:00:00', '2024-11-10 00:00:00', 2, 10),
(7, 7, '2024-11-15 00:00:00', '2024-12-05 00:00:00', '2024-12-15 00:00:00', 4, 9),
(8, 8, '2024-12-20 00:00:00', '2025-01-05 00:00:00', '2025-01-15 00:00:00', 2, 8),
(9, 9, '2025-01-22 00:00:00', '2025-02-01 00:00:00', '2025-02-10 00:00:00', 5, 6),
(16, 3, '2024-05-27 00:41:02', '2024-05-27 00:41:02', '2024-05-27 00:41:02', 1, 1),
(17, 15, '2024-05-27 00:00:00', '2024-05-28 00:00:00', '2024-05-29 00:00:00', 1, 1),
(18, 15, '2024-05-27 00:00:00', '2024-05-28 00:00:00', '2024-05-30 00:00:00', 1, 1),
(22, 15, '2024-05-27 00:00:00', '2024-05-28 00:00:00', '2024-05-31 00:00:00', 2, 2),
(23, 15, '2024-05-27 00:00:00', '2024-05-28 00:00:00', '2024-05-29 00:00:00', 2, 1),
(32, 15, '2024-05-28 00:00:00', '2024-05-30 00:00:00', '2024-05-31 00:00:00', 5, 9);

-- --------------------------------------------------------

--
-- Table structure for table `campingspace`
--

CREATE TABLE `campingspace` (
  `campingspace_id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `price_per_night` int(11) NOT NULL,
  `location_id` int(11) DEFAULT NULL,
  `longitude` decimal(10,7) DEFAULT NULL,
  `latitude` decimal(10,7) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `owner_id` int(11) NOT NULL,
  `is_available` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `campingspace`
--

INSERT INTO `campingspace` (`campingspace_id`, `name`, `price_per_night`, `location_id`, `longitude`, `latitude`, `address`, `owner_id`, `is_available`) VALUES
(1, 'Seaside Retreat', 100, 101, 4.9110000, 50.2580000, 'Seaside Retreat, Dinant', 2, 0),
(2, 'Urban Living', 150, 101, 4.9110000, 50.2580000, 'Urban Living, Dinant', 3, 1),
(3, 'Country House', 90, 101, 4.9110000, 50.2580000, 'Country House, Dinant', 2, 1),
(4, 'City Apartment', 200, 101, 4.9110000, 50.2580000, 'City Apartment, Dinant', 6, 1),
(5, 'Mountain Cabin', 120, 102, 5.9200000, 50.2330000, 'Mountain Cabin, Ardennes', 6, 1),
(6, 'Lakeside Villa', 130, 102, 5.9200000, 50.2330000, 'Lakeside Villa, Ardennes', 7, 1),
(7, 'Desert Flat', 85, 102, 5.9200000, 50.2330000, 'Desert Flat, Ardennes', 7, 1),
(8, 'Forest Lodge', 110, 102, 5.9200000, 50.2330000, 'Forest Lodge, Ardennes', 8, 1),
(9, 'Beachfront Bungalow', 200, 102, 5.9200000, 50.2330000, 'Beachfront Bungalow, Ardennes', 8, 1),
(10, 'Historical Downtown Loft', 175, 102, 5.9200000, 50.2330000, 'Historical Downtown Loft, Ardennes', 10, 1);

-- --------------------------------------------------------

--
-- Table structure for table `campingspace_amenities`
--

CREATE TABLE `campingspace_amenities` (
  `id` int(11) NOT NULL,
  `campingspace_id` int(11) DEFAULT NULL,
  `amenities_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `campingspace_amenities`
--

INSERT INTO `campingspace_amenities` (`id`, `campingspace_id`, `amenities_id`) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 1, 3),
(4, 2, 1),
(5, 2, 4),
(6, 2, 5),
(7, 3, 6),
(8, 3, 7),
(9, 4, 8),
(10, 4, 9),
(11, 4, 10),
(12, 5, 2),
(13, 5, 3),
(14, 6, 1),
(15, 6, 4),
(16, 7, 5),
(17, 7, 6),
(18, 8, 7),
(19, 8, 8),
(20, 9, 9),
(21, 9, 10),
(22, 10, 1),
(23, 10, 2);

-- --------------------------------------------------------

--
-- Table structure for table `image`
--

CREATE TABLE `image` (
  `image_id` int(11) NOT NULL,
  `imageUrl` varchar(255) DEFAULT NULL,
  `campingspace_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `image`
--

INSERT INTO `image` (`image_id`, `imageUrl`, `campingspace_id`) VALUES
(22, 'c1.jpg', 1),
(23, 'c12.jpg', 1),
(24, 'c21.jpg', 2),
(25, 'c22.jpg', 2),
(26, 'c31.jpg', 3),
(27, 'c32.jpg', 3),
(28, 'c41.jpg', 4),
(29, 'c42.jpg', 4),
(30, 'c51.jpg', 5),
(31, 'c52.jpg', 5),
(32, 'c61.jpg', 6),
(33, 'c62.jpg', 6),
(34, 'c71.jpg', 7),
(35, 'c72.jpg', 7),
(36, 'c81.jpg', 8),
(37, 'c82.jpg', 8),
(38, 'c91.jpg', 9),
(39, 'c92.jpg', 9),
(40, 'c101.jpg', 10),
(41, 'c102.jpg', 10);

-- --------------------------------------------------------

--
-- Table structure for table `location`
--

CREATE TABLE `location` (
  `location_id` int(11) NOT NULL,
  `country` varchar(255) DEFAULT NULL,
  `city` varchar(255) DEFAULT NULL,
  `postcode` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `location`
--

INSERT INTO `location` (`location_id`, `country`, `city`, `postcode`) VALUES
(101, 'Belgium', 'Dinant', '5500'),
(102, 'Belgium', 'Ardennes', '6800');

-- --------------------------------------------------------

--
-- Table structure for table `reviews`
--

CREATE TABLE `reviews` (
  `reviews_id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `reviewText` varchar(1000) DEFAULT NULL,
  `rating` int(11) DEFAULT NULL,
  `campingspace_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `reviews`
--

INSERT INTO `reviews` (`reviews_id`, `user_id`, `reviewText`, `rating`, `campingspace_id`) VALUES
(2, 3, 'Enjoyed the quiet surroundings.', 4, 2),
(3, 3, 'Could be better.', 3, 3),
(4, 7, 'Perfect for a weekend getaway.', 5, 4),
(6, 3, 'Lovely place, will visit again!', 5, 6),
(7, 7, 'Too expensive for the offered amenities.', 3, 7),
(8, 15, 'A truly unique experience.', 4, 8),
(9, 9, 'Great for families.', 4, 9),
(10, 9, 'Would not recommend.', 1, 10),
(11, 3, 'good', 3, 2),
(15, 3, 'Good', 5, 1),
(17, 9, 'good', 4, 10),
(19, 15, 'hbhjbh', 2, 1),
(20, 15, 'jbjhbh', 2, 1),
(21, 15, 'tehhehe', 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `user_id` int(11) NOT NULL,
  `user_name` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `first_name` varchar(255) NOT NULL,
  `last_name` varchar(255) NOT NULL,
  `phone_number` varchar(255) DEFAULT NULL,
  `dob` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`user_id`, `user_name`, `password`, `email`, `first_name`, `last_name`, `phone_number`, `dob`, `type`) VALUES
(2, 'janedoe', 'password123', 'jane.doe@email.com', 'Jane', 'Doe', '5551234568', '1985-06-10 00:00:00', 'host'),
(3, 'boblucas', 'password123', 'bob.lucas@email.com', 'Bob', 'Lucas', '5551234569', '1990-07-15 00:00:00', 'guest'),
(4, 'aliceryan', 'password123', 'alice.ryan@email.com', 'Alice', 'Ryan', '5551234570', '1988-08-20 00:00:00', 'host'),
(6, 'lisawhite', 'password123', 'lisa.white@email.com', 'Lisa', 'White', '5551234572', '1982-10-30 00:00:00', 'host'),
(7, 'mikeross', 'password123', 'mike.ross@email.com', 'Mike', 'Ross', '5551234573', '1975-11-05 00:00:00', 'guest'),
(8, 'nancylee', 'password123', 'nancy.lee@email.com', 'Nancy', 'Lee', '5551234574', '1992-12-10 00:00:00', 'host'),
(9, 'davidchen', 'password123', 'david.chen@email.com', 'David', 'Chen', '5551234575', '1994-01-15 00:00:00', 'guest'),
(10, 'sarahkhan', 'password123', 'sarah.khan@email.com', 'Sarah', 'Khan', '5551234576', '1986-02-20 00:00:00', 'host'),
(15, 'tt', 'tt', 'tt@email.com', 'tt', 'tt', '5461321658', '2013-06-12 00:00:00', 'guest'),
(16, 'ich', '77', 'gaireichchha2000@gmail.com', 'Ichchha', 'Gaire Sharma', '0477081732', '0001-01-01 00:00:00', 'host');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `amenities`
--
ALTER TABLE `amenities`
  ADD PRIMARY KEY (`amenities_id`);

--
-- Indexes for table `booking`
--
ALTER TABLE `booking`
  ADD PRIMARY KEY (`booking_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `campingspace`
--
ALTER TABLE `campingspace`
  ADD PRIMARY KEY (`campingspace_id`),
  ADD KEY `location_id` (`location_id`),
  ADD KEY `fk_owner` (`owner_id`);

--
-- Indexes for table `campingspace_amenities`
--
ALTER TABLE `campingspace_amenities`
  ADD PRIMARY KEY (`id`),
  ADD KEY `campingspace_id` (`campingspace_id`),
  ADD KEY `amenities_id` (`amenities_id`);

--
-- Indexes for table `image`
--
ALTER TABLE `image`
  ADD PRIMARY KEY (`image_id`),
  ADD KEY `campingspace_id` (`campingspace_id`);

--
-- Indexes for table `location`
--
ALTER TABLE `location`
  ADD PRIMARY KEY (`location_id`);

--
-- Indexes for table `reviews`
--
ALTER TABLE `reviews`
  ADD PRIMARY KEY (`reviews_id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `fk_campingspace` (`campingspace_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`user_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `amenities`
--
ALTER TABLE `amenities`
  MODIFY `amenities_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `booking`
--
ALTER TABLE `booking`
  MODIFY `booking_id` bigint(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=33;

--
-- AUTO_INCREMENT for table `campingspace`
--
ALTER TABLE `campingspace`
  MODIFY `campingspace_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT for table `campingspace_amenities`
--
ALTER TABLE `campingspace_amenities`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=73;

--
-- AUTO_INCREMENT for table `image`
--
ALTER TABLE `image`
  MODIFY `image_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=44;

--
-- AUTO_INCREMENT for table `location`
--
ALTER TABLE `location`
  MODIFY `location_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=103;

--
-- AUTO_INCREMENT for table `reviews`
--
ALTER TABLE `reviews`
  MODIFY `reviews_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `booking`
--
ALTER TABLE `booking`
  ADD CONSTRAINT `booking_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `campingspace`
--
ALTER TABLE `campingspace`
  ADD CONSTRAINT `campingspace_ibfk_1` FOREIGN KEY (`location_id`) REFERENCES `location` (`location_id`),
  ADD CONSTRAINT `fk_owner` FOREIGN KEY (`owner_id`) REFERENCES `user` (`user_id`);

--
-- Constraints for table `campingspace_amenities`
--
ALTER TABLE `campingspace_amenities`
  ADD CONSTRAINT `campingspace_amenities_ibfk_1` FOREIGN KEY (`campingspace_id`) REFERENCES `campingspace` (`campingspace_id`),
  ADD CONSTRAINT `campingspace_amenities_ibfk_2` FOREIGN KEY (`amenities_id`) REFERENCES `amenities` (`amenities_id`);

--
-- Constraints for table `image`
--
ALTER TABLE `image`
  ADD CONSTRAINT `image_ibfk_1` FOREIGN KEY (`campingspace_id`) REFERENCES `campingspace` (`campingspace_id`);

--
-- Constraints for table `reviews`
--
ALTER TABLE `reviews`
  ADD CONSTRAINT `fk_campingspace` FOREIGN KEY (`campingspace_id`) REFERENCES `campingspace` (`campingspace_id`),
  ADD CONSTRAINT `reviews_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `user` (`user_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
